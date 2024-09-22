
using Activities.Domain.DTO;
using Framework.Core.DomainObjects;

namespace Activities.Domain.DomainEvents
{
    public class WorkerActivityCreatedEvent : DomainEvent
    {
        public Guid ActivityId { get; set; }
        public string WorkerId { get; set; }

        public WorkerActivityCreatedEvent(Guid activityId,
                                   string workerId,CorrelationIdGuid correlationId):base(correlationId)
        {
            this.AggregateId = activityId;
            this.ActivityId = activityId;
            this.WorkerId = workerId;

        }
    }
}
