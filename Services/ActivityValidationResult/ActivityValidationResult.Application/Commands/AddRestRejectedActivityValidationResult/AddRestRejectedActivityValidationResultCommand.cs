
using FluentValidation.Results;
using Framework.Core.Messages;
using MassTransit;
using ActivityValidationResult.Domain.Enums;
using ActivityValidationResult.Application.Commands.AddActivityValidationResult;
using ActivityValidationResult.Domain.DomainEvents;
using Framework.Core.DomainObjects;

namespace ActivityValidationResult.Application.Commands.AddRestRejectedActivityValidationResult
{
    public class AddRestRejectedActivityValidationResultCommand : Command<AddRestRejectedActivityValidationResultCommandOutput>
    {

        public Guid ActivityId { get; private set; }
        public string WorkerId { get; private set; }
        public List<string> DescriptionErrors { get; set; }

        public AddRestRejectedActivityValidationResultCommand(Guid activityId,
                                                              string workerId,
                                                              List<string> descriptionErrors, CorrelationIdGuid correlationId):base(correlationId)
        {
            this.DescriptionErrors = descriptionErrors;
            ActivityId = activityId;
            WorkerId = workerId;
            this.AddValidCommand(new FluentValidation.Results.ValidationResult());
            this.AddRollBackEvent(new RestRejectedCompensation(this.ActivityId, WorkerId, correlationId));
        }
    }
}
