
using System.ComponentModel.DataAnnotations;
using Framework.Core.Messages;
using Activities.Domain.DomainEvents;
using Framework.Core.DomainObjects;

namespace Activities.Application.Commands.UpdateTimeStartAndTimeEndActivity
{
    public class ConfirmActivityCommand : Command<ConfirmActivityCommandOutput>
    {

        [Required]
        public Guid ActivityId {get;set;}



        public ConfirmActivityCommand(Guid activityId, CorrelationIdGuid correlationId):base(correlationId)
        {
            this.ActivityId = activityId;

            this.AddValidCommand(new ConfirmActivityCommandValidation().Validate(this));
            this.AddRollBackEvent(new ActivityConfirmedCompensationEvent(activityId, correlationId));

        }
    }
}
