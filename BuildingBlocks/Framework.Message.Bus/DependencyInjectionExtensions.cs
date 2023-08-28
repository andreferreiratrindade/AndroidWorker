using Microsoft.Extensions.DependencyInjection;
namespace Framework.MessageBus
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, string connection)
        {


            services.AddSingleton<IMessageBus>(new MessageBus(connection));


            return services;
        }
    }
}