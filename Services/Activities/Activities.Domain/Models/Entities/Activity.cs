using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Core.DomainObjects;
using Framework.Core.Messages;
using Activities.Domain.DomainEvents;
using Activities.Domain.Enums;
using Activities.Domain.Rules;
using Activities.Domain.ValidatorServices;
using Framework.Core.Notifications;
using FluentValidation.Results;

namespace Activities.Domain.Models.Entities
{
    public class Activity : Entity, IAggregateRoot
    {

        private readonly List<WorkActivity> _workers;
        public List<WorkActivity> GetWorkers() { return _workers; }
        public TypeActivityBuild TypeActivityBuild { get; }
        public DateTime TimeActivityStart { get; private set; }
        public DateTime TimeActivityEnd { get; private set; }
        public DateTime TimeRestEnd { get; private set; }
        
        public bool IsAlive { get; private set; }
        protected Activity()
        {

        }

        public static Activity Create(List<string> workers,
                                      TypeActivityBuild typeActivityBuild,
                                      DateTime timeActivityStart,
                                      DateTime timeActivityEnd)
        {
            var workersActivity = workers.Select(x => new WorkActivity(x)).ToList();
            var activity = new Activity(workersActivity, typeActivityBuild, timeActivityStart, timeActivityEnd);


            activity.AddEvent(new ActivityCreatedEvent(activity.Id,
                                                       workers,
                                                       activity.TypeActivityBuild,
                                                       activity.TimeActivityStart,
                                                       activity.TimeActivityEnd));
            return activity;
        }




        private Activity(List<WorkActivity> workers, TypeActivityBuild typeActivityBuild, DateTime timeActivityStart, DateTime timeActivityEnd)
        {
            _workers = workers;
            TypeActivityBuild = typeActivityBuild;
            TimeActivityStart = timeActivityStart;
            TimeActivityEnd = timeActivityEnd;
            TimeRestEnd = DateTime.Now;
            IsAlive = true;

            //Apply
        }

     

        public void Inactivate()
        {
            this.IsAlive = false;
            AddEvent(new ActivityInativatedEvent(Id));
        }

        public void UpdateTimeStartAndTimeEnd(DateTime timeActivityStart, DateTime timeActivityEnd, IActivityValidatorService activityValidatorService, IDomainNotification domainNotification)
        {
            TimeActivityEnd = timeActivityEnd;
            TimeActivityStart = timeActivityStart;

            AddEvent(new ActivityUptatedTimeStartAndTimeEndEvent(Id,
                                                       TimeActivityStart,
                                                       TimeActivityEnd));
        }

    }
}