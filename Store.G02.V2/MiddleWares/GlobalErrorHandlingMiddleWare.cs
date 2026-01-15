using Shared.ErrorModels;

namespace Store.G02.V2.MiddleWares
{
    public class GlobalErrorHandlingMiddleWare
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
               await _next.Invoke(context);
            }catch(Exception ex)
            {
                //Logic
                //1-set status code
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                //2-set content type
                context.Response.ContentType = "application/json";
                //3-set response body
                var response = new ErrorDetails
                {
                    StatusCode = context.Response.StatusCode,
                    ErrorMessage = ex.Message
                };
                //4-return response

                await context.Response.WriteAsJsonAsync(response);
            }

        }
    }
}
