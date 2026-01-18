using Shared.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions.Baskets
{
    public interface IBasketService
    {
        Task<BasketDto?> GetBasketAsync(string basketid);
        Task<BasketDto?> CreateBasketAsync(BasketDto dto,TimeSpan duration);
        Task<bool> DeleteBasketAsync(string basketid);
    }
}
