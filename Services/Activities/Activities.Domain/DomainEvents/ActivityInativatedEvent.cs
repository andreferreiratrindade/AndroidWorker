using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Core.Messages;
using Activities.Domain.Enums;

namespace Activities.Domain.DomainEvents
{
    public class ActivityInativatedEvent : Event
    {
        private Guid ActivityId;

        public ActivityInativatedEvent(Guid activityId)
        {
            this.ActivityId = activityId;
        }
    }
}