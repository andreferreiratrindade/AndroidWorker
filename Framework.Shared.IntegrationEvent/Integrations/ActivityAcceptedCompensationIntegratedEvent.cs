using EasyNetQ;
using Framework.Core.DomainObjects;
using Framework.Core.Messages.Integration;
using Framework.Core.Notifications;

namespace Framework.Shared.IntegrationEvent.Integration
{

    [Queue("ActivityRejected", ExchangeName = "Activity")]

    public class ActivityAcceptedCompensationIntegratedEvent : Framework.Core.Messages.Integration.IntegrationEvent
    {
        public  List<string> Notifications { get; private set; }
        public Guid ActivityId { get; private set; }


        public ActivityAcceptedCompensationIntegratedEvent(Guid activityId,
                                                           List<string> notifications,
                                                           CorrelationIdGuid correlationId) :base(correlationId)
        {
            ActivityId = activityId;
            this.Notifications = notifications;
        }

    }
}
