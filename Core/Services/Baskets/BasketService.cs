using AutoMapper;
using Domain.Contracts;
using Domain.Entites.Baskets;
using Domain.Exceptions.NotFound;
using Services.Abstractions.Baskets;
using Shared.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Baskets
{
    public class BasketService(IBasketRepository _basketrepository,IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto?> CreateBasketAsync(BasketDto dto, TimeSpan duration)
        {
            var basket = _mapper.Map<CustomerBasket>(dto);
           var result =  await  _basketrepository.CreateBasketAsync(basket, duration);
            return _mapper.Map<BasketDto>(result);


        }
        public async Task<BasketDto?> GetBasketAsync(string basketid)
        {
           var basket = await _basketrepository.GetBasketAsync(basketid);
            

           var result = _mapper.Map<BasketDto>(basket);

            return result;
        }

        public async Task<bool> DeleteBasketAsync(string basketid)
        {
           return await _basketrepository.DeleteBasketAsync(basketid);
        }

    }
}
