using ActivityValidationResult.Domain.Enums;
using Framework.Core.DomainObjects;

namespace ActivityValidationResult.Domain.DomainEvents
{
    public class UpdatedActivityConfirmedEvent : DomainEvent
    {
        public Guid ActivityId { get; set; }
        public TypeStatus TypeStatus { get; set; }

        public UpdatedActivityConfirmedEvent(Guid activityId,TypeStatus typeStatus, CorrelationIdGuid correlationId) : base(correlationId)
        {
            this.TypeStatus = typeStatus;
            ActivityId = activityId;
        }
    }
}
