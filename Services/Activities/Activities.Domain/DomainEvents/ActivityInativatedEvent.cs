
using Framework.Core.DomainObjects;

namespace Activities.Domain.DomainEvents
{
    public class ActivityInativatedEvent : DomainEvent
    {
        private Guid ActivityId;

        public ActivityInativatedEvent(Guid activityId,CorrelationIdGuid correlationId):base(correlationId)
        {
            this.ActivityId = activityId;
            this.AggregateId = activityId;
        }
    }
}
