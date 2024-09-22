using Framework.Core.DomainObjects;
using ActivityValidationResult.Domain.DomainEvents;
using ActivityValidationResult.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Framework.Shared.IntegrationEvent.Enums;

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
                    TypeActivityBuild typeActivityBuild,
                    DateTime timeActivityStart,
                    DateTime timeActivityEnd,
                    List<string> workers,
                    CorrelationIdGuid correlationId)
        {


            var @event = new ActivityValidationResultAddedEvent(
                                            Guid.NewGuid(),
                                            activityId,
                                            typeActivityBuild,
                                            timeActivityStart,
                                            timeActivityEnd,
                                            TypeStatus.Pending,
                                            workers,
                                            correlationId);
            this.RaiseEvent(@event);
        }

        public static ActivityValidationResultEntity Create(Guid activityId,
                    TypeActivityBuild typeActivityBuild,
                    DateTime timeActivityStart,
                    DateTime timeActivityEnd,
                    List<string> workers,
                    CorrelationIdGuid CorrelationId)
        {
            var ActivityValidationResult = new ActivityValidationResultEntity(activityId,
                                                typeActivityBuild,
                                                timeActivityStart,
                                                timeActivityEnd,
                                                 workers,
                                                 CorrelationId);
            return ActivityValidationResult;
        }

        protected override void When(IDomainEvent @event)
        {
            switch (@event)
            {
                case ActivityValidationResultAddedEvent x: OnActivityValidationResultAddedEvent(x); break;
                case RestAcceptedEvent x: OnAddRestAccepted(x); break;
                case ActivityRejectedEvent x: OnActivityRejectedEvent(x); break;
                case ActivityAcceptedEvent x: OnActivityAcceptedEvent(x); break;
                case AddedRestRejectedEvent x: OnAddedRestRejectedEvent(x); break;
                case UpdatedActivityConfirmedEvent x: OnUpdatedActivityConfirmedEvent(x); break;
            }
        }



        private void OnActivityValidationResultAddedEvent(ActivityValidationResultAddedEvent x)
        {
            AggregateId = x.AggregateId;
            ActivityId = x.ActivityId;
            Workers = x.Workers;
            Status = x.TypeStatus;
        }

        public void AddRestAccepted(Guid restId, DateTime timeRestStart, DateTime timeRestEnd, string workerId, CorrelationIdGuid correlationId)
        {

            var @event = new RestAcceptedEvent(restId,
                                            workerId,
                                            timeRestStart,
                                            timeRestEnd,
                                            TypeStatus.Accepted,
                                            ActivityId, correlationId);
            this.RaiseEvent(@event);
        }

        private void OnAddRestAccepted(RestAcceptedEvent x)
        {
            Rests.Add(new RestEntity(x.RestId, x.WorkerId, x.Status));
        }

        private void OnAddedRestRejectedEvent(AddedRestRejectedEvent x)
        {
            Rests.Add(new RestEntity(x.WorkerId, x.Status, x.DescriptionErros));
        }

        private void OnActivityRejectedEvent(ActivityRejectedEvent x)
        {
            this.Status = x.TypeStatus;
        }

        private void OnActivityAcceptedEvent(ActivityAcceptedEvent x)
        {
            this.Status = x.TypeStatus;
        }

        public void TryFinishValidation(CorrelationIdGuid correlationId)
        {
            if (HasFinishedValidationRest())
            {
                DomainEvent? @event = null;
                if (ExistsRestRejected())
                {
                    var restRejectedDescription = Rests.Where(x => x.TypeStatus == TypeStatus.Rejected).SelectMany(x => x.DescriptionErrors).ToList();
                    @event = new ActivityRejectedEvent(ActivityId, TypeStatus.Rejected, restRejectedDescription, correlationId);
                }
                else
                {
                    @event = new ActivityAcceptedEvent(ActivityId, TypeStatus.Accepted, correlationId);
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

        public void AddRestRejected(string workerId, List<string> descriptionErrors, CorrelationIdGuid correlationId)
        {
            var @event = new AddedRestRejectedEvent(workerId, TypeStatus.Rejected, this.ActivityId, descriptionErrors, correlationId);
            this.RaiseEvent(@event);
        }

        public void UpdateActivityConfirmed(CorrelationIdGuid correlationId)
        {
            var @event = new UpdatedActivityConfirmedEvent(this.ActivityId, TypeStatus.Confirmed, correlationId);
            this.RaiseEvent(@event);

        }
        private void OnUpdatedActivityConfirmedEvent(UpdatedActivityConfirmedEvent x)
        {
            this.Status = x.TypeStatus;
        }

    }
}
