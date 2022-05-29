using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pulse.Tracks.Core.Database;
using Pulse.Tracks.Core.Repositories;

namespace Pulse.Tracks.Database
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DatabaseContext>(opt => opt.UseSqlServer(connectionString));

            services.AddTransient<IDatabaseContext, DatabaseContext>();

            services.AddTransient<IRepositoriesManager, RepositoriesManager>();

            return services;
        }
    }
}
