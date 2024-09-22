using Framework.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rests.Domain.DomainEvents
{
    public class RestAddedCompensationEvent : RollBackEvent
    {
        public Guid ActivityId { get; }
        public string WorkerId { get;  }

        public RestAddedCompensationEvent(Guid activityId, string workerId, CorrelationIdGuid correlationId): base(correlationId)
        {
            this.WorkerId = workerId;
            this.ActivityId = activityId;
        }
    }
}
