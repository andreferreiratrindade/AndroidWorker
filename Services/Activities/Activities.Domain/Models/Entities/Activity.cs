using Framework.Core.DomainObjects;

using Activities.Domain.DomainEvents;

using Activities.Domain.ValidatorServices;
using Framework.Shared.IntegrationEvent.Enums;


namespace Activities.Domain.Models.Entities
{
    public class Activity : AggregateRoot, IAggregateRoot
    {

        private List<WorkActivity> _workers { get; set; } = new List<WorkActivity>();
        public List<WorkActivity> GetWorkers() { return _workers; }
        public TypeActivityBuild TypeActivityBuild { get; private set; }
        public DateTime TimeActivityStart { get; private set; }
        public DateTime TimeActivityEnd { get; private set; }
        public TypeActivityStatus Status { get; private set; }
        protected Activity()
        {

        }

        public static Activity Create(List<string> workers,
                                      TypeActivityBuild typeActivityBuild,
                                      DateTime timeActivityStart,
                                      DateTime timeActivityEnd,
                                      CorrelationIdGuid correlationId)
        {
            var workersActivity = workers.Select(x => new WorkActivity(x)).ToList();
            var activity = new Activity(workers, typeActivityBuild, timeActivityStart, timeActivityEnd, correlationId);

            return activity;
        }




        private Activity(List<string> workers,
                         TypeActivityBuild typeActivityBuild,
                         DateTime timeActivityStart,
                         DateTime timeActivityEnd,
                         CorrelationIdGuid correlationId)
        {

            var @event = new ActivityCreatedEvent(Guid.NewGuid(),
                                                    workers,
                                                    typeActivityBuild,
                                                    timeActivityStart,
                                                    timeActivityEnd,
                                                    TypeActivityStatus.Created,
                                                    correlationId );
            this.RaiseEvent(@event);

            var @eventsWorkers = workers.Select(x=> new WorkerActivityCreatedEvent(@event.ActivityId, x,correlationId)).ToList();
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
                case ActivityInativatedEvent x: OnActivityInativatedEvent(x); break;
                case ActivityUptatedTimeStartAndTimeEndEvent x: OnActivityUptatedTimeStartAndTimeEndEvent(x); break;
                case ActivityConfirmedEvent x: OnActivityConfirmedEvent(x); break;
            }
        }

        private void OnActivityCreatedEvent(ActivityCreatedEvent @event)
        {
            AggregateId = @event.ActivityId;
            TypeActivityBuild = @event.TypeActivityBuild;
            TimeActivityStart = @event.TimeActivityStart;
            TimeActivityEnd = @event.TimeActivityEnd;
            Status = @event.Status;
        }

        private void OnWorkerActivityCreatedEvent(WorkerActivityCreatedEvent @event)
        {
            _workers.Add(new WorkActivity(@event.WorkerId));
        }

        private void OnActivityInativatedEvent(ActivityInativatedEvent @event)
        {
            Status = TypeActivityStatus.Deleted;
        }

        private void OnActivityUptatedTimeStartAndTimeEndEvent(ActivityUptatedTimeStartAndTimeEndEvent @event)
        {
            TimeActivityEnd = @event.TimeActivityEnd;
            TimeActivityStart = @event.TimeActivityStart;
        }

        private void OnActivityConfirmedEvent(ActivityConfirmedEvent @event)
        {
            Status = @event.Status;
        }


        public void Inactivate(CorrelationIdGuid correlationId)
        {
            this.RaiseEvent(new ActivityInativatedEvent(this.AggregateId,correlationId));

        }

        public void UpdateTimeStartAndTimeEnd(DateTime timeActivityStart,
            DateTime timeActivityEnd,
            IActivityValidatorService activityValidatorService,
            CorrelationIdGuid correlationId)
        {
            this.RaiseEvent(new ActivityUptatedTimeStartAndTimeEndEvent(AggregateId , TimeActivityStart,
                                                       TimeActivityEnd,correlationId));
        }

        public void ConfirmActivity(CorrelationIdGuid correlationId)
        {

            this.RaiseEvent(new ActivityConfirmedEvent(this.AggregateId,
                                                    _workers.Select(x=> x.WorkerId).ToList(),
                                                    TypeActivityBuild,
                                                    TimeActivityStart,
                                                    TimeActivityEnd,
                                                    TypeActivityStatus.Confirmmed,correlationId));

        }

        public void RejectActivity(CorrelationIdGuid correlationId)
        {

            this.RaiseEvent(new ActivityRejectedEvent(this.AggregateId,
                                                    _workers.Select(x => x.WorkerId).ToList(),
                                                    TypeActivityBuild,
                                                    TimeActivityStart,
                                                    TimeActivityEnd,
                                                    TypeActivityStatus.Rejected,correlationId));

        }


    }
}
