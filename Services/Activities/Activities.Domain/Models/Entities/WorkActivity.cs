using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Core.DomainObjects;
using Framework.Core.Messages;


namespace Activities.Domain.Models.Entities
{
    public class WorkActivity : Entity
    {
        public WorkActivity(string workerId){
            this.WorkerId = workerId;
        }
        public WorkActivity(Guid activityId, string workerId)
        {
            this.ActivityId = activityId;
            this.WorkerId = workerId;
        }
        public Guid ActivityId { get; set; }
        public string WorkerId { get; set; }
            
    }
}