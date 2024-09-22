
using FluentValidation.Results;
using Framework.Core.DomainObjects;
using Framework.Core.Messages;
using MassTransit;
using Rests.Domain.DomainEvents;
using Rests.Domain.Enums;

namespace Rests.Application.Commands.AddRest
{
    public class AddRestIntegratedCommand : Command<AddRestCommandOutput>
    {
        public Guid ActivityId {get;set;}
        public TypeActivityBuild TypeActivityBuild { get; set; }
        public DateTime TimeRestStart { get; set; }
        public DateTime TimeActivityStart { get; set; }
        public string WorkerId { get; set;}

        public AddRestIntegratedCommand(Guid activityId,
                                        TypeActivityBuild typeActivityBuild,
                                        DateTime timeActivityStart,
                                        DateTime timeRestStart,
                                        string workerId, CorrelationIdGuid correlationId):base(correlationId)
        {
            this.ActivityId = activityId;
            this.TypeActivityBuild = typeActivityBuild;
            this.TimeRestStart = timeRestStart;
            this.TimeActivityStart = timeActivityStart;
            this.WorkerId = workerId;
            this.AddValidCommand(new FluentValidation.Results.ValidationResult());
            this.AddRollBackEvent(new RestAddedCompensationEvent( activityId, workerId, correlationId));
        }
    }
}
