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
    public class WorkerActivityCreatedEvent : DomainEvent
    {
        public Guid ActivityId { get; set; }
        public string WorkerId { get; set; }

        public WorkerActivityCreatedEvent(Guid activityId,
                                   string workerId)
        {
            this.AggregateId = activityId;
            this.ActivityId = activityId;
            this.WorkerId = workerId;
           
        }
    }
}