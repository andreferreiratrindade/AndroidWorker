
using System.ComponentModel.DataAnnotations;
using Framework.Core.DomainObjects;
using Framework.Core.Messages;

namespace Activities.Application.Commands.UpdateTimeStartAndTimeEndActivity
{
    public class UpdateTimeStartAndTimeEndActivityCommand : Command<UpdateTimeStartAndTimeEndActivityCommandOutput>
    {

        [Required]
        public Guid ActivityId { get; set; }
        [Required]
        public DateTime TimeActivityStart { get; set; }
        [Required]
        public DateTime TimeActivityEnd { get; set; }


        public UpdateTimeStartAndTimeEndActivityCommand(Guid activityId,
                                                        DateTime timeActivityStart,
                                                        DateTime timeActivityEnd,
                                                        CorrelationIdGuid correlationId) :base(correlationId)
        {
            this.TimeActivityStart = timeActivityStart;
            this.TimeActivityEnd = timeActivityEnd;
            this.ActivityId = activityId;
         //   this.AddValidCommand(new UpdateTimeStartAndTimeEndActivityCommandValidation().Validate(this));
         //   this.AddCommandOutput(new UpdateTimeStartAndTimeEndActivityCommandOutput());
        }
    }
}
