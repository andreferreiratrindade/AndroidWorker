using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Core.Messages;
using Activities.Domain.Enums;

namespace Activities.Application.Commands.UpdateTimeStartAndTimeEndActivity
{
    public class UpdateTimeStartAndTimeEndActivityCommandOutput
    {
        public Guid ActivityId {get;set;}
        public TypeActivityBuild TypeActivityBuild { get;set; }
        public DateTime TimeActivityStart { get; set; }
        public DateTime TimeActivityEnd { get;set; }
        public DateTime TimeRestEnd { get;set; }
    }
}