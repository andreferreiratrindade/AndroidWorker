
using System.ComponentModel.DataAnnotations;
using Framework.Core.Messages;
using Activities.Domain.Enums;

namespace Activities.Application.Commands.UpdateTimeStartAndTimeEndActivity
{
    public class ConfirmActivityCommand : Command<ConfirmActivityCommandOutput>
    {

        [Required]
        public Guid ActivityId {get;set;}
        


        public ConfirmActivityCommand(Guid activityId)
        {
            this.ActivityId = activityId;

            ValidCommand(new ConfirmActivityCommandValidation().Validate(this));

        }
    }
}
