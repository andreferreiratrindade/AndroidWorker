using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Framework.Core.Messages;
using Rests.Domain.Enums;

namespace Rests.Application.Commands.UpdateTimeStartAndEndRest
{
    public class UpdateTimeStartAndEndRestCommand: Command<Result>
    {
        [Required]

        public Guid ActivityId {get;set;}
        [Required]
        public DateTime TimeActivityStart { get; set; }
        [Required]
        public DateTime TimeActivityEnd { get; set; }

        public UpdateTimeStartAndEndRestCommand(Guid activityId,   DateTime timeActivityStart, DateTime timeActivityEnd)
        {   
            this.ActivityId = activityId;
            this.TimeActivityStart = timeActivityStart;
            this.TimeActivityEnd = timeActivityEnd;

            // ValidCommand(new AddActivityCommandValidation().Validate(this));
        }
    }
}