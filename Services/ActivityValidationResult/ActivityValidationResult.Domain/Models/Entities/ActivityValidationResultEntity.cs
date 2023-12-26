using Framework.Core.DomainObjects;
using ActivityValidationResult.Domain.DomainEvents;
using ActivityValidationResult.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Diagnostics.CodeAnalysis;

namespace ActivityValidationResult.Domain.Models.Entities
{
    public class ActivityValidationResultEntity : AggregateRoot, IAggregateRoot
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get { return this.AggregateId.ToString(); } }
        public Guid ActivityId { get; private set; }
        public TypeStatus Status { get; private set; }
        public List<string> Workers { get; set; } = new List<string>();
        public List<RestEntity> Rests { get; set; } = new List<RestEntity>();

        protected ActivityValidationResultEntity() { }

        private ActivityValidationResultEntity(Guid activityId,
                     List<string> workers,
                     Guid? CorrelationId)
        {
            Guid correlationId = (CorrelationId ?? Guid.Empty);

            var @event = new ActivityValidationResultAddedEvent(Guid.NewGuid(),
                                            activityId,
                                            TypeStatus.Pending,
                                            correlationId);
            this.RaiseEvent(@event);

            var @eventsWorkers = workers.Select(x => new WorkerActivityCreatedEvent(x, correlationId)).ToList();
            @eventsWorkers.ForEach(x =>
            {
                this.RaiseEvent(x);
            });
        }

        public static ActivityValidationResultEntity Create(Guid activityId,
                          List<string> workers,
                          Guid? CorrelationId
                          )
        {
            var ActivityValidationResult = new ActivityValidationResultEntity(activityId, workers, CorrelationId);
            return ActivityValidationResult;
        }

        protected override void When(IDomainEvent @event)
        {
            switch (@event)
            {
                case ActivityValidationResultAddedEvent x: OnActivityValidationResultAddedEvent(x); break;
                case WorkerActivityCreatedEvent x: OnWorkerActivityCreatedEvent(x); break;
                case AddedRestAcceptedEvent x: OnAddRestAccepted(x); break;
                case ActivityRejectedEvent x: OnActivityRejectedEvent(x); break;
                case ActivityAcceptedEvent x: OnActivityAcceptedEvent(x); break;
                case AddedRestRejectedEvent x: OnAddedRestRejectedEvent(x); break;


            }
        }
        private void OnWorkerActivityCreatedEvent(WorkerActivityCreatedEvent @event)
        {
            Workers.Add(@event.WorkerId);
        }


        private void OnActivityValidationResultAddedEvent(ActivityValidationResultAddedEvent x)
        {
            AggregateId = x.AggregateId;
            ActivityId = x.ActivityId;
        }

        public void AddRestAccepted(Guid restId, DateTime timeRestStart, DateTime timeRestEnd, string workerId)
        {

            var @event = new AddedRestAcceptedEvent(restId,
                                            workerId,
                                            timeRestStart,
                                            timeRestEnd,
                                            TypeStatus.Accepted,
                                            ActivityId);
            this.RaiseEvent(@event);
        }

        private void OnAddRestAccepted(AddedRestAcceptedEvent x)
        {
            Rests.Add(new RestEntity(x.RestId, x.WorkerId, x.Status));
        }

        private void OnAddedRestRejectedEvent(AddedRestRejectedEvent x)
        {
            Rests.Add(new RestEntity(x.RestId, x.WorkerId, x.Status, x.DescriptionErros));
        }

        private void OnActivityRejectedEvent(ActivityRejectedEvent x)
        {
            this.Status = x.TypeStatus;
        }

        private void OnActivityAcceptedEvent(ActivityAcceptedEvent x)
        {
            this.Status = x.TypeStatus;
        }

        public void TryFinishValidation()
        {
            if (HasFinishedValidationRest())
            {
                DomainEvent? @event = null;
                if (ExistsRestRejected())
                {
                    var restRejectedDescription = Rests.Where(x => x.TypeStatus == TypeStatus.Rejected).SelectMany(x => x.DescriptionErrors).ToList();
                    @event = new ActivityRejectedEvent(ActivityId, TypeStatus.Rejected, restRejectedDescription);
                }
                else
                {
                    @event = new ActivityAcceptedEvent(ActivityId, TypeStatus.Accepted);
                }
                this.RaiseEvent(@event);
            }
        }

        private bool HasFinishedValidationRest()
        {
            return Rests.Count == Workers.Count;
        }

        private bool ExistsRestRejected()
        {
            return Rests.Any(x => x.TypeStatus == TypeStatus.Rejected);
        }
    }
}