using MassTransit;
using Microsoft.Extensions.DependencyInjection;
namespace Framework.MessageBus
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, string connection)
        {

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(connection);
                });
            });

            //services.AddSingleton<IMessageBus>(new MessageBus());


            return services;
        }
    }
}