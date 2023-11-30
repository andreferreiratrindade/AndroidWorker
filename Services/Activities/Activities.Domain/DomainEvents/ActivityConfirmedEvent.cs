using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Core.Messages;
using Activities.Domain.Enums;
using Framework.Core.DomainObjects;

namespace Activities.Domain.DomainEvents
{
    public class ActivityConfirmedEvent : DomainEvent
    {
        public Guid ActivityId { get; private set; }
        public string WorkId { get; private set; }
        public Guid RestId { get; private set; }
        public TypeActivityBuild TypeActivityBuild { get; private set; }
        public DateTime TimeActivityStart { get; private set; }
        public DateTime TimeActivityEnd { get; private set; }

        public ActivityConfirmedEvent(Guid activityId,
                                    string workId,
                                    TypeActivityBuild typeActivityBuild,
                                    DateTime timeActivityStart,
                                    DateTime timeActivityEnd,
                                    Guid restId)
        {
            this.AggregateId = activityId;
            this.ActivityId = activityId;
            this.WorkId = workId;
            this.RestId = restId;
            this.TimeActivityStart = timeActivityStart;
            this.TimeActivityEnd = timeActivityEnd;
            this.TypeActivityBuild = typeActivityBuild;
        }
    }
}