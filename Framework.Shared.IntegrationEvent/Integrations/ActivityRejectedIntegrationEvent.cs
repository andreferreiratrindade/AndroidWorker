using EasyNetQ;
using Framework.Core.DomainObjects;
using Framework.Core.Messages.Integration;
using Framework.Core.Notifications;

namespace Framework.Shared.IntegrationEvent.Integration
{

    [Queue("ActivityRejected", ExchangeName = "Activity")]

    public class ActivityRejectedIntegrationEvent : Framework.Core.Messages.Integration.IntegrationEvent
    {
        public Guid ActivityId { get; private set; }
        public List<string> DescriptionError { get; private set; }


        public ActivityRejectedIntegrationEvent(Guid activityId, List<string> descriptionError, CorrelationIdGuid correlationId):base(correlationId)
        {
            ActivityId = activityId;
            DescriptionError = descriptionError;
        }

    }
}
