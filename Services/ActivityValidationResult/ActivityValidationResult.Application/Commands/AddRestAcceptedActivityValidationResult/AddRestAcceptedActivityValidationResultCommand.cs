
using FluentValidation.Results;
using Framework.Core.Messages;
using MassTransit;
using ActivityValidationResult.Domain.Enums;
using ActivityValidationResult.Application.Commands.AddActivityValidationResult;
using ActivityValidationResult.Domain.DomainEvents;
using Framework.Core.DomainObjects;

namespace ActivityValidationResult.Application.Commands.AddRestAcceptedActivityValidationResult
{
    public class AddRestAcceptedActivityValidationResultCommand : Command<AddRestAcceptedActivityValidationResultCommandOutput>
    {

        public Guid ActivityId { get; private set; }
        public Guid RestId { get; private set; }
        public string WorkerId { get; private set; }
        public DateTime TimeRestStart { get; private set; }
        public DateTime TimeRestEnd { get; private set; }

        public AddRestAcceptedActivityValidationResultCommand(Guid activityId,
                                                              Guid restId,
                                                              string workerId,
                                                              DateTime timeRestStart,
                                                              DateTime timeRestEnd, CorrelationIdGuid correlationId):base(correlationId)
        {
            ActivityId = activityId;
            RestId = restId;
            WorkerId = workerId;
            TimeRestStart = timeRestStart;
            TimeRestEnd = timeRestEnd;
            this.AddValidCommand(new FluentValidation.Results.ValidationResult());
            this.AddRollBackEvent(new RestAcceptedCompensationEvent(this.ActivityId, this.WorkerId, correlationId));
        }
    }
}
