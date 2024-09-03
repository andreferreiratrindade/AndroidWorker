using EasyNetQ;
using Framework.Core.Messages.Integration;
namespace Framework.Shared.IntegrationEvent.Integration
{

    [Queue("ActivityConfirmedCompensation", ExchangeName = "ActivityExchange")]

    public class ActivityConfirmedCompensationIntegrationEvent : Framework.Core.Messages.Integration.IntegrationEvent
    {
       public ActivityConfirmedCompensationIntegrationEvent( Guid correlationId, List<Core.Notifications.NotificationMessage> notifications)
        {
            CorrelationId = correlationId;
            Notifications = notifications;

        }
        public Guid CorrelationById { get { return this.CorrelationId; } }

       List<Core.Notifications.NotificationMessage> Notifications {get;}

    }
}
