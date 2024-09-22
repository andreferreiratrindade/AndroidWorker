
using System.ComponentModel.DataAnnotations;
using Framework.Core.DomainObjects;
using Framework.Core.Messages;

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


        public RejectActivityCommand(Guid activityId, CorrelationIdGuid correlationId):base(correlationId)
        {
            this.ActivityId = activityId;

            this.AddValidCommand(new RejectActivityCommandValidation().Validate(this));
                    //    this.AddRollBackEvent(new RejectActivityCCompensationEvent(this.ActivityId));


        }
    }
}
