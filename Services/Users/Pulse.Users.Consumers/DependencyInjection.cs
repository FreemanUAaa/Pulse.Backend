using Pulse.Users.Database;
using Pulse.Users.Application;
using MassTransit;
using Pulse.Users.Consumers.Consumers;

namespace Pulse.Users.Consumers
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddConsumers(this IServiceCollection services, IConfiguration configuration)
        {
            string connection = configuration.GetConnectionString("Default");

            services.AddDatabase(connection);
            services.AddApplication(configuration);
            services.AddMassTransit(x =>
            {
                x.AddConsumer<CreateCoverConsumer>();
                x.AddConsumer<DeleteCoverConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}
