using Framework.Core.Messages;
using ActivityValidationResult.Domain.DomainEvents;
using Framework.Core.DomainObjects;

namespace ActivityValidationResult.Application.Commands.UpdateActivityConfirmed
{
    public class UpdateActivityConfirmedCommand : Command<UpdateActivityConfirmedCommandOutput>
    {
        public Guid ActivityId {get;set;}



        public UpdateActivityConfirmedCommand(Guid activityId, CorrelationIdGuid correlationId):base(correlationId)
        {
            this.ActivityId = activityId;
            this.AddValidCommand(new FluentValidation.Results.ValidationResult());
            this.AddRollBackEvent(new ActivityAcceptedCompensationEvent(this.ActivityId,correlationId));

        }
    }
}
