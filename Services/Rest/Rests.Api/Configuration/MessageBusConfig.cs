using Framework.MessageBus;
using Rests.Api.IntegrationService;

namespace Rests.Api.Configuration

{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {

            services.AddMessageBus(configuration.GetSection("MessageQueueConnection").Value)
               .AddHostedService<ActivityUptatedTimeStartAndTimeEndEventIntegrationHandler>();
        }
    }
}