
using FluentValidation.Results;
using Framework.Core.Messages;
using MassTransit;
using ActivityValidationResult.Domain.Enums;
using System.Text.Json.Serialization;

namespace ActivityValidationResult.Application.Commands.AddActivityValidationResult
{
    public class AddActivityValidationResultCommand : Command<AddActivityValidationResultCommandOutput>, CorrelatedBy<Guid>
    {
        public Guid ActivityId {get;set;}

        public List<string> Workers { get; set;}


        public Guid CorrelationId { get; private set;}

        public AddActivityValidationResultCommand(Guid activityId,
                                        List<string> workers,
                                        Guid correlationId)
        {
            this.ActivityId = activityId;
            this.Workers = workers;
            this.CorrelationId = correlationId;
            this.AddValidCommand(new FluentValidation.Results.ValidationResult());
            this.AddCommandOutput(new AddActivityValidationResultCommandOutput());

        }
    }
}
