
using System.ComponentModel.DataAnnotations;
using Framework.Core.Messages;
using Activities.Domain.Enums;

namespace Activities.Application.Commands.RejectActivity
{
    public class RejectActivityCommand : Command<RejectActivityCommandOutput>
    {

        [Required]
        public Guid ActivityId {get;set;}

        [Required]
        public Guid RestId { get; set; }

        [Required]
        public string WorkId { get; set; }


        public RejectActivityCommand(Guid activityId)
        {
            this.ActivityId = activityId;

            this.AddValidCommand(new RejectActivityCommandValidation().Validate(this));
            this.AddCommandOutput(new RejectActivityCommandOutput());

        }
    }
}
