
using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;

namespace Activities.Domain.DomainEvents
{
    public class ActivityCreatedEvent : DomainEvent
    {
        public  Guid ActivityId {get;set;}
        public List<string> Workers { get; set;}
        public  TypeActivityBuild TypeActivityBuild {get;set;}
        public  DateTime TimeActivityStart {get;set;}
        public  DateTime TimeActivityEnd{get;set;}
        public TypeActivityStatus Status { get; }

        public ActivityCreatedEvent(Guid activityId,
                                    List<string> workers,
                                    TypeActivityBuild typeActivityBuild,
                                    DateTime timeActivityStart,
                                    DateTime timeActivityEnd,
                                    TypeActivityStatus status,CorrelationIdGuid correlationId):base(correlationId)
        {
            this.AggregateId = activityId;
            this.Workers = workers;
            this.ActivityId = activityId;
            this.TypeActivityBuild = typeActivityBuild;
            this.TimeActivityStart = timeActivityStart;
            this.TimeActivityEnd = timeActivityEnd;
            Status = status;
        }
    }
}
