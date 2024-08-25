
using System.ComponentModel.DataAnnotations;
using Framework.Core.Messages;

namespace Activities.Application.Commands.DeleteActivity
{
    public class DeleteActivityCommand : Command<Result>
    {

        [Required]
        public Guid ActivityId {get;set;}


        public DeleteActivityCommand(Guid activityId)
        {
             ActivityId = activityId;
             this.AddValidCommand(new DeleteActivityCommandValidation().Validate(this));
            this.AddCommandOutput(new Result());

        }
    }
}
