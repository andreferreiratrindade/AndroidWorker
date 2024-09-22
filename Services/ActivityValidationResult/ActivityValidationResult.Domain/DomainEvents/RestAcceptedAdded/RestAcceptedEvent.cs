using Framework.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityValidationResult.Domain.DomainEvents
{
    public class RestAcceptedEvent : DomainEvent
    {
        public Guid ActivityId { get;}
        public string WorkerId { get; }
        public DateTime TimeRestStart { get; }
        public Guid RestId { get;  }
        public DateTime TimeRestEnd { get; }
        public Enums.TypeStatus Status { get; }

        public RestAcceptedEvent(Guid restId,
                                      string workerId,
                                      DateTime timeRestStart,
                                      DateTime timeRestEnd,
                                      Enums.TypeStatus status,
                                    Guid activityId, CorrelationIdGuid correlationId):base(correlationId)

        {
            this.Status = status;
            this.TimeRestEnd = timeRestEnd;
            this.RestId = restId;
            this.TimeRestStart = timeRestStart;
            this.TimeRestStart = timeRestEnd;
            this.WorkerId = workerId;
            this.ActivityId = activityId;
        }
    }

    public class RestAcceptedCompensationEvent: RollBackEvent
    {
        public RestAcceptedCompensationEvent(Guid activityId, string workerId, CorrelationIdGuid correlationId):base(correlationId){
            ActivityId = activityId;
            WorkerId = workerId;
        }
        public Guid ActivityId { get;}
        public string WorkerId { get; }

    }
}
