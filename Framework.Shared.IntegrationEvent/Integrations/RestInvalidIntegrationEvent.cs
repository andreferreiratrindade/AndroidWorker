using EasyNetQ;
using Framework.Core.Notifications;

namespace Framework.Shared.IntegrationEvent.Integration
{

    [Queue("RestNotAdded", ExchangeName = "RestExchange")]

    public class RestInvalidIntegrationEvent : Framework.Core.Messages.Integration.IntegrationEvent
    {
       public List<NotificationMessage> Notifications { get; }
       public RestInvalidIntegrationEvent(Guid correlationId, List<NotificationMessage> notifications)
        {
            
            CorrelationId = correlationId;
            Notifications = notifications;
        }
        public Guid CorrelationById { get { return this.CorrelationId; } }

    }
}