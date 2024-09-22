
using FluentValidation.Results;
using Framework.Core.Messages;
using MassTransit;
using ActivityValidationResult.Domain.Enums;
using System.Text.Json.Serialization;
using ActivityValidationResult.Domain.DomainEvents;
using Framework.Shared.IntegrationEvent.Enums;
using Framework.Core.DomainObjects;

namespace ActivityValidationResult.Application.Commands.AddActivityValidationResult
{
    public class AddActivityValidationResultCommand : Command<AddActivityValidationResultCommandOutput>
    {
        public Guid ActivityId {get;set;}

        public List<string> Workers { get; set;}

        public  TypeActivityBuild TypeActivityBuild{ get; private set;}

        public  DateTime TimeActivityStart{ get; private set;}

        public  DateTime TimeActivityEnd{ get; private set;}


        public AddActivityValidationResultCommand(Guid activityId,
                                        TypeActivityBuild typeActivityBuild,
                                        DateTime timeActivityStart,
                                        DateTime timeActivityEnd,
                                        List<string> workers, CorrelationIdGuid correlationId):base(correlationId)
        {
            this.ActivityId = activityId;
            this.Workers = workers;
            this.TimeActivityEnd = timeActivityEnd;
            this.TimeActivityStart = timeActivityStart;
            this.TypeActivityBuild = typeActivityBuild;
            this.AddValidCommand(new FluentValidation.Results.ValidationResult());
            this.AddRollBackEvent(new ActivityAcceptedCompensationEvent(activityId, correlationId));
        }
    }
}
