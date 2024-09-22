
using Framework.Core.DomainObjects;

namespace Activities.Domain.DomainEvents
{
    public class ActivityUptatedTimeStartAndTimeEndEvent : DomainEvent
    {
        public Guid ActivityId { get; private set; }
        public DateTime TimeActivityStart { get; private set; }
        public DateTime TimeActivityEnd { get; private set; }

        public ActivityUptatedTimeStartAndTimeEndEvent(Guid activityId,
                                    DateTime timeActivityStart,
                                    DateTime timeActivityEnd,CorrelationIdGuid correlationId):base(correlationId)
        {
            this.AggregateId = activityId;
            this.ActivityId = activityId;
            this.TimeActivityStart = timeActivityStart;
            this.TimeActivityEnd = timeActivityEnd;
        }
    }
}
