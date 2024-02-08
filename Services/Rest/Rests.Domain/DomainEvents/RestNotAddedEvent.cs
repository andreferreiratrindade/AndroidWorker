using Framework.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rests.Domain.DomainEvents
{
    public class RestRejectedEvent : RollBackEvent
    {
        public Guid ActivityId { get; }
        public string WorkerId { get;  }

        public RestRejectedEvent(Guid activityId, string workerId)
        {
            this.WorkerId = workerId;
            this.ActivityId = activityId;
            CorrelationId = activityId;
        }
    }
}
