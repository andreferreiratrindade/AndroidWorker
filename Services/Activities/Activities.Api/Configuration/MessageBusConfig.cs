using Framework.MessageBus;
namespace Activities.Api.Configuration

{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
             services.AddMessageBus(configuration["MessageQueueConnection"]);
        }
    }
}