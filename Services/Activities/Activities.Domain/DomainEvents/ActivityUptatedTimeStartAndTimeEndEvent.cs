using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Core.Messages;
using Activities.Domain.Enums;
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
                                    DateTime timeActivityEnd)
        {
            this.AggregateId = activityId;
            this.ActivityId = activityId;
            this.TimeActivityStart = timeActivityStart;
            this.TimeActivityEnd = timeActivityEnd;
        }
    }
}