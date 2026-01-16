
using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Periestence;
using Periestence.Data.Contexts;
using Services;
using Services.Abstractions.Services;
using Services.Mapping.Products;
using Services.Services;
using Shared.ErrorModels;
using Store.G02.V2.Extensions;
using Store.G02.V2.MiddleWares;
using System.Threading.Tasks;

namespace Store.G02.V2
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAllServices(builder.Configuration);

            var app = builder.Build();


            await app.ConfigureMiddleWaresAsync();

            app.Run();
        }
    }
}
