using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Core.Messages;
using Activities.Domain.Enums;

namespace Activities.Domain.DomainEvents
{
    public class ActivityUptatedTimeStartAndTimeEndEvent : Event
    {
        public Guid ActivityId { get; private set; }
        public DateTime TimeActivityStart { get; private set; }
        public DateTime TimeActivityEnd { get; private set; }

        public ActivityUptatedTimeStartAndTimeEndEvent(Guid activityId,
                                    DateTime timeActivityStart,
                                    DateTime timeActivityEnd)
        {
            this.ActivityId = activityId;
            this.TimeActivityStart = timeActivityStart;
            this.TimeActivityEnd = timeActivityEnd;
        }
    }
}