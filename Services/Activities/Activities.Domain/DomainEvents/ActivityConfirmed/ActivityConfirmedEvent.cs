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
        public List<string> Workers { get; private set; }
        public TypeActivityBuild TypeActivityBuild { get; private set; }
        public DateTime TimeActivityStart { get; private set; }
        public DateTime TimeActivityEnd { get; private set; }
        public TypeActivityStatus Status { get; private set; }

        public ActivityConfirmedEvent(Guid activityId,
                                    List<string> workers,
                                    TypeActivityBuild typeActivityBuild,
                                    DateTime timeActivityStart,
                                    DateTime timeActivityEnd,
                                    TypeActivityStatus status)
        {
            this.AggregateId = activityId;
            this.ActivityId = activityId;
            this.TimeActivityStart = timeActivityStart;
            this.TimeActivityEnd = timeActivityEnd;
            this.TypeActivityBuild = typeActivityBuild;
            this.Workers = workers;
            this.Status = status;
        }
    }
}