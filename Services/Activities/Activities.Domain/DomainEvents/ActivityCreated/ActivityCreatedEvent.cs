using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Core.Messages;
using Activities.Domain.Enums;
using Activities.Domain.DTO;
using Framework.Core.DomainObjects;

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
                                    TypeActivityStatus status,
                                    Guid correlationId)
        {
            this.CorrelationId = correlationId;
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