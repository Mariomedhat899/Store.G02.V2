using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions.Services;
using Services.Mapping.Auth;
using Services.Mapping.Baskets;
using Services.Mapping.Orders;
using Services.Mapping.Products;
using Services.Services;

namespace Services
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddAutoMapper(M => M.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(M => M.AddProfile(new BasketProfile()));
            services.AddAutoMapper(M => M.AddProfile(new OrderProfile()));
            services.AddAutoMapper(M => M.AddProfile(new AuthProfile()));

            return services;
        }
    }
}
