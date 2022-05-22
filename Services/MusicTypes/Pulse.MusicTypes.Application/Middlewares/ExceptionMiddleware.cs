using Microsoft.AspNetCore.Http;
using Pulse.MusicTypes.Application.ViewModels;

namespace Pulse.MusicTypes.Application.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next) => this.next = next;

        public async Task Invoke(HttpContext http)
        {
            try
            {
                await next.Invoke(http);
            }
            catch (Exception error)
            {
                http.Response.StatusCode = StatusCodes.Status400BadRequest;
                http.Response.ContentType = "application/json; charset=utf-8";
                ErrorResponseVm responseVm = new(error.Message);
                await http.Response.WriteAsync(responseVm.ToString());
            }
        }
    }
}
