using EasyNetQ;
using Framework.Core.Notifications;

namespace Framework.Shared.IntegrationEvent.Integration
{

    [Queue("RestRejected", ExchangeName = "RestExchange")]

    public class RestRejectedIntegrationEvent : Framework.Core.Messages.Integration.IntegrationEvent
    {
       public List<NotificationMessage> Notifications { get; }
       public RestRejectedIntegrationEvent(Guid correlationId, List<NotificationMessage> notifications)
        {
            
            CorrelationId = correlationId;
            Notifications = notifications;
        }
        public Guid CorrelationById { get { return this.CorrelationId; } }

    }
}