
using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;

namespace Activities.Domain.DomainEvents
{
    public class ActivityConfirmedEvent : DomainEvent
    {
        public Guid ActivityId { get; private set; }
        public List<string> Workers { get; private set; }
        public TypeActivityBuild TypeActivityBuild { get; private set; }
        public DateTime TimeActivityStart { get; private set; }
        public DateTime TimeActivityEnd { get; private set; }
        public TypeActivityStatus Status { get; private set; }

        public ActivityConfirmedEvent(Guid activityId,
                                    List<string> workers,
                                    TypeActivityBuild typeActivityBuild,
                                    DateTime timeActivityStart,
                                    DateTime timeActivityEnd,
                                    TypeActivityStatus status,
                                    CorrelationIdGuid correlationId):base(correlationId)
        {
            this.AggregateId = activityId;
            this.ActivityId = activityId;
            this.TimeActivityStart = timeActivityStart;
            this.TimeActivityEnd = timeActivityEnd;
            this.TypeActivityBuild = typeActivityBuild;
            this.Workers = workers;
            this.Status = status;
        }
    }
}
