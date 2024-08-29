
using FluentValidation.Results;
using Framework.Core.Messages;
using MassTransit;
using Rests.Domain.Enums;

namespace Rests.Application.Commands.AddRest
{
    public class AddRestIntegratedCommand : Command<AddRestCommandOutput>, CorrelatedBy<Guid>
    {
        public Guid ActivityId {get;set;}
        public TypeActivityBuild TypeActivityBuild { get; set; }
        public DateTime TimeRestStart { get; set; }
        public DateTime TimeActivityStart { get; set; }
        public string WorkerId { get; set;}

        public Guid CorrelationId { get; private set;}

        public AddRestIntegratedCommand(Guid activityId,
                                        TypeActivityBuild typeActivityBuild,
                                        DateTime timeActivityStart,
                                        DateTime timeRestStart,
                                        string workerId,
                                        Guid correlationId)
        {
            this.ActivityId = activityId;
            this.TypeActivityBuild = typeActivityBuild;
            this.TimeRestStart = timeRestStart;
            this.TimeActivityStart = timeActivityStart;
            this.WorkerId = workerId;
            this.CorrelationId = correlationId;
            this.AddValidCommand(new FluentValidation.Results.ValidationResult());
            this.AddCommandOutput(new AddRestCommandOutput());
        }
    }
}
