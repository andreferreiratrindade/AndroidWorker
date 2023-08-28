
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
            ValidCommand(new DeleteActivityCommandValidation().Validate(this));
        }
    }
}
