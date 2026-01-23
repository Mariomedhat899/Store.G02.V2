using Domain.Contracts;
using Domain.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;
using Periestence;
using Periestence.Identity.Contexts;
using Services;
using Shared;
using Shared.ErrorModels;
using Store.G02.V2.MiddleWares;
using System.Text;

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

            services.AddIdentityServices();

            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

            services.AddAuthServices(configuration);




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
        private static IServiceCollection AddAuthServices(this IServiceCollection services,IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))


                };
            });

            return services;
        }
        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentityCore<AppUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }).AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<IdentityStoreDbContext>();

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

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }

        private static async Task<WebApplication> SeedData(this WebApplication app)
        {
            var scope = app.Services.CreateScope();

            var DbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();

            await DbInitializer.InitializeAsync();
            await DbInitializer.InitializeIdentityAsync();
            return app;
        }

        private static WebApplication UseGlobalErrorHandling(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleWare>();
            return app;
        }



    }
}
