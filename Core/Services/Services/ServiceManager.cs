using AutoMapper;
using Domain.Contracts;
using Services.Abstractions.Baskets;
using Services.Abstractions.Cache;
using Services.Abstractions.Products;
using Services.Abstractions.Services;
using Services.Baskets;
using Services.Cahce;
using Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ServiceManager(IUnitOfWork _unitOfWork,IMapper _mapper,IBasketRepository _basketRepository,ICacheRepository _cacheRepository) : IServiceManager
    {
        public IProductService ProductService { get; } =  new ProductService(_unitOfWork,_mapper);

        public IBasketService BasketService { get; } = new BasketService(_basketRepository, _mapper);

        public ICacheService CacheService { get; } = new CacheService(_cacheRepository);
    }
}
