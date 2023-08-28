using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Core.Messages;
using Activities.Domain.Enums;

namespace Activities.Domain.DomainEvents
{
    public class ActivityCreatedEvent : Event
    {
        public  Guid ActivityId {get;set;}
        public  List<string> Workers {get;set;}
        public  TypeActivityBuild TypeActivityBuild {get;set;}
        public  DateTime TimeActivityStart {get;set;}
        public  DateTime TimeActivityEnd{get;set;}

        public ActivityCreatedEvent(Guid activityId,
                                    List<string> workers,
                                    TypeActivityBuild typeActivityBuild,
                                    DateTime timeActivityStart,
                                    DateTime timeActivityEnd)
        {
            this.ActivityId = activityId;
            this.Workers = workers;
            this.TypeActivityBuild = typeActivityBuild;
            this.TimeActivityStart = timeActivityStart;
            this.TimeActivityEnd = timeActivityEnd;
        }
    }
}