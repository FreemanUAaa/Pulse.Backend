using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pulse.MusicTypes.Core.Database;

namespace Pulse.MusicTypes.Database
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DatabaseContext>(opt => opt.UseSqlServer(connectionString));

            services.AddTransient<IDatabaseContext, DatabaseContext>();

            return services;
        }
    }
}
