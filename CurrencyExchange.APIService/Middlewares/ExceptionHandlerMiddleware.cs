using System.Net;

namespace CurrencyExchange.APIService.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        public readonly ILogger<ExceptionHandlerMiddleware> logger;
        public readonly RequestDelegate next;


        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger,RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var erroId = new Guid();
                logger.LogError(ex,$"{erroId} :{ex.Message}");
                httpContext.Response.StatusCode =(int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";
                var error = new {Id= erroId,ErrorMessage="Something went wrong!.We are looking into it." };
                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
