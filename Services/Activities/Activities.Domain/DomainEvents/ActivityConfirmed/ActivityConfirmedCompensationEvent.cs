
using Framework.Core.DomainObjects;
using MassTransit;

namespace Activities.Domain.DomainEvents
{
    public class ActivityConfirmedCompensationEvent : RollBackEvent
    {
        public Guid ActivityId  { get; set; }
        public ActivityConfirmedCompensationEvent(Guid activityId, CorrelationIdGuid correlationId):base(correlationId)
        {
            ActivityId = activityId;
        }
    }
}
