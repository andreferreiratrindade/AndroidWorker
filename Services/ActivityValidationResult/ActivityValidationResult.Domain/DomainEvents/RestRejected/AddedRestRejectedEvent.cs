using Framework.Core.DomainObjects;

namespace ActivityValidationResult.Domain.DomainEvents
{
    public class AddedRestRejectedEvent : DomainEvent
    {
        public Guid ActivityId { get; }
        public string WorkerId { get; }
        public Enums.TypeStatus Status { get; }
        public List<string> DescriptionErros { get; }

        public AddedRestRejectedEvent(string workerId,
                                      Enums.TypeStatus status,
                                      Guid activityId,
                                      List<string> descriptionErros, CorrelationIdGuid correlationId) : base(correlationId)
        {
            this.Status = status;
            this.WorkerId = workerId;
            this.ActivityId = activityId;
            this.DescriptionErros = descriptionErros;


        }
    }

    public class RestRejectedCompensation : RollBackEvent
    {
        public RestRejectedCompensation(Guid activityId, string workerId, CorrelationIdGuid correlationId) : base(correlationId)
        {
            ActivityId = activityId;
            WorkerId = workerId;
        }
        public Guid ActivityId { get; }
        public string WorkerId { get; }
    }
}
