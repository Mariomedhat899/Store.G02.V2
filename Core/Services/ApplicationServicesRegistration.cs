using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions.Services;
using Services.Mapping.Baskets;
using Services.Mapping.Products;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection services,IConfiguration configuration)
        {

           services.AddScoped<IServiceManager, ServiceManager>();
           services.AddAutoMapper(M => M.AddProfile(new ProductProfile(configuration)));
           services.AddAutoMapper(M => M.AddProfile(new BasketProfile()));

            return services;
        }
    }
}
