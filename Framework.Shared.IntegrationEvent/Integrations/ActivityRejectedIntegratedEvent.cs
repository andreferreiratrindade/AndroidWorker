using EasyNetQ;
using Framework.Core.Messages.Integration;
using Framework.Core.Notifications;

namespace Framework.Shared.IntegrationEvent.Integration
{

    [Queue("ActivityRejected", ExchangeName = "Activity")]

    public class ActivityRejectedIntegratedEvent : Framework.Core.Messages.Integration.IntegrationEvent
    {
        public  List<string> Notifications { get; private set; }
        public Guid ActivityId { get; private set; }


        public ActivityRejectedIntegratedEvent(Guid correlationId, Guid activityId,  List<string> notifications)
        {
            CorrelationId = correlationId;
            ActivityId = activityId;
            this.Notifications = notifications;
        }

    }
}