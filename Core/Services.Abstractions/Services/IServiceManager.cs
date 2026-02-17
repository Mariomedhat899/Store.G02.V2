using Services.Abstractions.Auth;
using Services.Abstractions.Baskets;
using Services.Abstractions.Cache;
using Services.Abstractions.Orders;
using Services.Abstractions.Products;

namespace Services.Abstractions.Services
{
    public interface IServiceManager
    {
        IProductService ProductService { get; }

        IBasketService BasketService { get; }
        ICacheService CacheService { get; }
        IAuthService AuthService { get; }

        IOrderService OrderService { get; }
    }
}
