using Microsoft.AspNetCore.Builder;
using Pulse.MusicTypes.Application.Middlewares;

namespace Pulse.MusicTypes.Application
{
    public static class Middleware
    {
        public static IApplicationBuilder AddApplicationMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            return app;
        }
    }
}
