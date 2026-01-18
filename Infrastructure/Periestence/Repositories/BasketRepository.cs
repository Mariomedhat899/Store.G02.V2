using Domain.Contracts;
using Domain.Entites.Baskets;
using Domain.Exceptions.BadRequest;
using Domain.Exceptions.NotFound;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Periestence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _redis = connection.GetDatabase();
        public async Task<CustomerBasket> GetBasketAsync(string id)
        {
            var Redis= await _redis.StringGetAsync(id);

            if (Redis.IsNullOrEmpty) throw new BasketNotFoundExeption($"This Basket With id:{id} Was Not Found !!");

            var Basket = JsonSerializer.Deserialize<CustomerBasket>(Redis);

            if(Basket is null) throw new JsonException($"Failed to deserialize basket data for Id: {id}. The data may be corrupted.");
            return Basket;
            
        }
        public async Task<CustomerBasket> CreateBasketAsync(CustomerBasket basket, TimeSpan duration)
        {
            var RedisValue = JsonSerializer.Serialize(basket);
          var flag = await _redis.StringSetAsync(basket.Id, RedisValue, duration);

            if (!flag) throw new BasketBadRequestException();

            return await GetBasketAsync(basket.Id);

        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            var flag = await _redis.KeyExistsAsync(id);
            if (!flag) throw new DeleteBasketBadRequestException(id);
            return flag;
        }

    }
}
