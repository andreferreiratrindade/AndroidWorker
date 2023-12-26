using Framework.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityValidationResult.Domain.DomainEvents
{
    public class AddedRestAcceptedEvent : DomainEvent
    {
        public Guid ActivityId { get;}
        public string WorkerId { get; }
        public DateTime TimeRestStart { get; }
        public Guid RestId { get;  }
        public DateTime TimeRestEnd { get; }
        public Enums.TypeStatus Status { get; }

        public AddedRestAcceptedEvent(Guid restId,
                                      string workerId,
                                      DateTime timeRestStart,
                                      DateTime timeRestEnd,
                                      Enums.TypeStatus status,
                                      Guid activityId)
        {
            this.Status = status;
            this.TimeRestEnd = timeRestEnd;
            this.RestId = restId;
            this.TimeRestStart = timeRestStart;
            this.TimeRestStart = timeRestEnd;
            this.WorkerId = workerId;
            this.ActivityId = activityId;
             CorrelationId = restId;
        }
    }
}
