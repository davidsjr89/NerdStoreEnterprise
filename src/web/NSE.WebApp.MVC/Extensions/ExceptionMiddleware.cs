using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CustomHttpRequestException ex)
            {
                HandleRequestExceptionAsync(httpContext, ex);
            }
        }

        private static void HandleRequestExceptionAsync(HttpContext httpContext, CustomHttpRequestException httpRequestException)
        {
            if(httpRequestException.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                httpContext.Response.Redirect($"/login?ReturnUrl={httpContext.Request.Path}");
                return;
            }
            httpContext.Response.StatusCode = (int)httpRequestException.StatusCode;
        }
    }
}
