using EasyNetQ;
using Framework.Core.Messages.Integration;
namespace Framework.Shared.IntegrationEvent.Integration
{

    [Queue("ActivityUptatedTimeStartAndTimeEnd", ExchangeName = "ActivityExchange")]

    public class ActivityNotCreatedIntegrationEvent : Framework.Core.Messages.Integration.IntegrationEvent
    {
       public ActivityNotCreatedIntegrationEvent( Guid correlationId)
        {
            CorrelationId = correlationId;
        }
        public Guid CorrelationById { get { return this.CorrelationId; } }

    }
}