using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Periestence;
using Services;
using Shared.ErrorModels;
using Store.G02.V2.MiddleWares;

namespace Store.G02.V2.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddAllServices(this IServiceCollection services,IConfiguration configuration)
        {
            // Add services to the container.
            services.AddWebServices();

            services.AddInfrastructeServices(configuration);

            services.ApplicationServices(configuration);

            services.ConfigureApiBehaviourOptions();


            return services;
        }

        private static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
        private static IServiceCollection ConfigureApiBehaviourOptions(this IServiceCollection services)
        {
             services.Configure<ApiBehaviorOptions>(configure =>
            {
                configure.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(M => M.Value.Errors.Any())
                                                         .Select(M => new ValdationError()
                                                         {
                                                             FieldName = M.Key,
                                                             Errors = M.Value.Errors.Select(E => E.ErrorMessage)
                                                         }).ToList();
                    var response = new ValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            }
                        );


            return services;
        }






        public static async Task<WebApplication> ConfigureMiddleWaresAsync(this WebApplication app)
        {


            app.UseStaticFiles();
            
            app.UseGlobalErrorHandling();

            await app.SeedData();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            return app;
        }

        private static async Task<WebApplication> SeedData(this WebApplication app)
        {
            var scope = app.Services.CreateScope();

            var DbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();

            await DbInitializer.InitializeAsync();
            return app;
        }

        private static WebApplication UseGlobalErrorHandling(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleWare>();
            return app;
        }



    }
}
