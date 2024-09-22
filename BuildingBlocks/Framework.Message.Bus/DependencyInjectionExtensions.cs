using EasyNetQ;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
namespace Framework.MessageBus
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services)
        {


            services.AddScoped<IMessageBus, MessageBus>();

            return services;
        }
    }
}
