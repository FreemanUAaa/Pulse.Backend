using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Pulse.Users.Producers.Interfaces;
using Pulse.Users.Producers.Services;

namespace Pulse.Users.Producers
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProducers(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq();
            });

            services.AddTransient<IUserProducer, UserProducer>();

            return services;
        }
    }
}
