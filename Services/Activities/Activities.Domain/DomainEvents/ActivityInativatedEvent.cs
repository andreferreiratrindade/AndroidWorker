using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Core.Messages;
using Activities.Domain.Enums;
using Framework.Core.DomainObjects;

namespace Activities.Domain.DomainEvents
{
    public class ActivityInativatedEvent : DomainEvent
    {
        private Guid ActivityId;

        public ActivityInativatedEvent(Guid activityId)
        {
            this.ActivityId = activityId;
            this.AggregateId = activityId;
        }
    }
}