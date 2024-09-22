using EasyNetQ;
using Framework.Core.DomainObjects;
using Framework.Core.Messages.Integration;
namespace Framework.Shared.IntegrationEvent.Integration
{

    [Queue("ActivityConfirmedCompensation", ExchangeName = "ActivityExchange")]

    public class ActivityConfirmedCompensationIntegrationEvent : Framework.Core.Messages.Integration.IntegrationEvent
    {
       public ActivityConfirmedCompensationIntegrationEvent(  List<Core.Notifications.NotificationMessage> notifications, CorrelationIdGuid correlationId):base(correlationId)
        {
            Notifications = notifications;

        }
       List<Core.Notifications.NotificationMessage> Notifications {get;}

    }
}
