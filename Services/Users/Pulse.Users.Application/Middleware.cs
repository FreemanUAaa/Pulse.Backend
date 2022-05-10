using Microsoft.AspNetCore.Builder;
using Pulse.Users.Application.Middlewares;

namespace Pulse.Users.Application
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
