using Domain.Contracts;
using Domain.Entites.Baskets;
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

            if (Redis.IsNullOrEmpty) return null;

            var Basket = JsonSerializer.Deserialize<CustomerBasket>(Redis);

            if(Basket is null) return null;
            return Basket;
            
        }
        public async Task<CustomerBasket> CreateBasketAsync(CustomerBasket basket, TimeSpan duration)
        {
            var RedisValue = JsonSerializer.Serialize(basket);
          var flag = await _redis.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket),duration);

            if (!flag) return null;

            return await GetBasketAsync(basket.Id);

        }

        public Task<bool> DeleteBasketAsync(string id)
        {
          return  _redis.KeyDeleteAsync(id);
        }

    }
}
