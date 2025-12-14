using AutoMapper;
using Domain.Contracts;
using Services.Abstractions.Products;
using Services.Abstractions.Services;
using Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class SeerviceManager(IUnitOfWork _unitOfWork,IMapper _mapper) : IServiceManager
    {
        public IProductService ProductService { get; } =  new ProductService(_unitOfWork,_mapper);
    }
}
