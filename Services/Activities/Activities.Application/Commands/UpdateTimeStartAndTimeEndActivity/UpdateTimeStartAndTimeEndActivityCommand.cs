
using System.ComponentModel.DataAnnotations;
using Framework.Core.Messages;
using Activities.Domain.Enums;

namespace Activities.Application.Commands.UpdateTimeStartAndTimeEndActivity
{
    public class UpdateTimeStartAndTimeEndActivityCommand : Command<UpdateTimeStartAndTimeEndActivityCommandOutput>
    {

        [Required]
        public Guid ActivityId {get;set;}
        [Required]
        public DateTime TimeActivityStart { get;set;}
        [Required]
        public DateTime TimeActivityEnd { get; set;}


        public UpdateTimeStartAndTimeEndActivityCommand(Guid activityId, DateTime timeActivityStart, DateTime timeActivityEnd)
        {
            this.TimeActivityStart = timeActivityStart;
            this.TimeActivityEnd = timeActivityEnd;
            this.ActivityId = activityId;

            ValidCommand(new UpdateTimeStartAndTimeEndActivityCommandValidation().Validate(this));

        }
    }
}
