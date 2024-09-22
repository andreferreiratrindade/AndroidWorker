
using System.ComponentModel.DataAnnotations;
using Framework.Core.DomainObjects;
using Framework.Core.Messages;

namespace Activities.Application.Commands.DeleteActivity
{
    public class DeleteActivityCommand : Command<Result>
    {

        [Required]
        public Guid ActivityId {get;set;}


        public DeleteActivityCommand(Guid activityId, CorrelationIdGuid correlationId):base(correlationId)
        {
             ActivityId = activityId;
             this.AddValidCommand(new DeleteActivityCommandValidation().Validate(this));
                        // this.AddRollBackEvent(new DeleteActivityCompensationEvent(this.ActivityId));


        }
    }
}
