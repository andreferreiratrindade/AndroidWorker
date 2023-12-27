using EasyNetQ;
using Framework.Core.Messages.Integration;
using Framework.Core.Notifications;

namespace Framework.Shared.IntegrationEvent.Integration
{

    [Queue("ActivityAccepted", ExchangeName = "Activity")]

    public class ActivityAcceptedIntegrationEvent : Framework.Core.Messages.Integration.IntegrationEvent
    {
        public Guid ActivityId { get; private set; }


        public ActivityAcceptedIntegrationEvent(Guid activityId)
        {
            CorrelationId = activityId;
            ActivityId = activityId;
        }

    }
}