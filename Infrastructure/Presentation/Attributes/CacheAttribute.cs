using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions.Cache;
using Services.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation.Attributes
{
    public class CacheAttribute(int TimeInSeconds) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var CacheService = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CacheService;

            var cacheKey = GenerateCacheKey(context.HttpContext.Request);

            var Result = await CacheService.GetAsync(cacheKey);

            if (!string.IsNullOrEmpty(Result))
            {
                var Response = new ContentResult()
                {
                    Content = Result,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = Response;

                return;
            }

             var actionContext =  await next.Invoke();

            if (actionContext.Result is OkObjectResult okObjectResult)
            {
               await CacheService.SetAsync(cacheKey, okObjectResult.Value, TimeSpan.FromSeconds(TimeInSeconds));
            }
        }


        private string GenerateCacheKey(HttpRequest request)
        {
            var Key = new StringBuilder();

            Key.Append(request.Path);

            foreach (var item in request.Query)
            {
                Key.Append($"{item.Key}-{item.Value}");
                
            }

            return Key.ToString();


        }
    }
}
