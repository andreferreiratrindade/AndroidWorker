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
using System.Diagnostics;
using Activities.Domain.DTO;
using Microsoft.Extensions.Logging;

namespace Activities.Domain.Models.Entities
{
    public class Activity : AggregateRoot, IAggregateRoot
    {

        private List<WorkActivity> _workers { get; set; } = new List<WorkActivity>();
        public List<WorkActivity> GetWorkers() { return _workers; }
        public TypeActivityBuild TypeActivityBuild { get; private set; }
        public DateTime TimeActivityStart { get; private set; }
        public DateTime TimeActivityEnd { get; private set; }
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
            var activity = new Activity(workers, typeActivityBuild, timeActivityStart, timeActivityEnd);

            return activity;
        }




        private Activity(List<string> workers,
                         TypeActivityBuild typeActivityBuild,
                         DateTime timeActivityStart,
                         DateTime timeActivityEnd)
        {

            var @event = new ActivityCreatedEvent(Guid.NewGuid(),
                                                    typeActivityBuild,
                                                    timeActivityStart,
                                                    timeActivityEnd);
            this.RaiseEvent(@event);

            var @eventsWorkers = workers.Select(x=> new WorkerActivityCreatedEvent(@event.ActivityId, x)).ToList();
            @eventsWorkers.ForEach(x =>
            {
                this.RaiseEvent(x);
            });
        }

        protected override void When(IDomainEvent @event)
        {
            switch (@event)
            {
                case ActivityCreatedEvent x: OnActivityCreatedEvent(x); break;
                case WorkerActivityCreatedEvent x: OnWorkerActivityCreatedEvent(x); break;
            }
        }

        private void OnActivityCreatedEvent(ActivityCreatedEvent @event)
        {
            AggregateId = @event.ActivityId;
            TypeActivityBuild = @event.TypeActivityBuild;
            TimeActivityStart = @event.TimeActivityStart;
            TimeActivityEnd = @event.TimeActivityEnd;
            IsAlive = true;
        }

        private void OnWorkerActivityCreatedEvent(WorkerActivityCreatedEvent @event)
        {
            _workers.Add(new WorkActivity(@event.WorkerId));
        }


        public void Inactivate()
        {
            this.IsAlive = false;
            this.RaiseEvent(new ActivityInativatedEvent(this.AggregateId));

        }

        public void UpdateTimeStartAndTimeEnd(DateTime timeActivityStart, DateTime timeActivityEnd,
            IActivityValidatorService activityValidatorService, IDomainNotification domainNotification)
        {
            TimeActivityEnd = timeActivityEnd;
            TimeActivityStart = timeActivityStart;

            this.RaiseEvent(new ActivityUptatedTimeStartAndTimeEndEvent(AggregateId , TimeActivityStart,
                                                       TimeActivityEnd));

        }

    }
}