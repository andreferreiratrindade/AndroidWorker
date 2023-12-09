
using System.ComponentModel.DataAnnotations;
using Framework.Core.Messages;
using Activities.Domain.Enums;

namespace Activities.Application.Commands.UpdateTimeStartAndTimeEndActivity
{
    public class ConfirmActivityWorkerCommand : Command<ConfirmActivityWorkerCommandOutput>
    {

        [Required]
        public Guid ActivityId {get;set;}
        
        [Required]
        public Guid RestId { get; set; }
        
        [Required]
        public string WorkId { get; set; }


        public ConfirmActivityWorkerCommand(Guid activityId, Guid restId, string workerId)
        {
            this.RestId = restId;
            this.WorkId = workerId;
            this.ActivityId = activityId;

            ValidCommand(new ConfirmActivityWorkerCommandValidation().Validate(this));

        }
    }
}
