using Framework.Core.DomainObjects;
using Rests.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rests.Domain.DomainEvents
{
    public class RestAddedEvent : DomainEvent
    {

        public RestAddedEvent(Guid aggregateId,
                              Guid activityId,
                              string workerId,
                              DateTime timeRestStart,
                              DateTime timeRestEnd,
                              TypeActivityBuild typeActivityBuild,
                              bool isAlive,
                              CorrelationIdGuid correlationId): base(correlationId)
        {
            RestId = aggregateId;
            ActivityId = activityId;
            WorkerId = workerId;
            TimeRestStart = timeRestStart;
            TimeRestEnd = timeRestEnd;
            TypeActivityBuild = typeActivityBuild;
            IsAlive = isAlive;
        }

        public string WorkerId { get; private set; }
        public Guid RestId { get; private set; }
        public Guid ActivityId { get; private set; }
        public DateTime TimeRestStart { get; private set; }
        public DateTime TimeRestEnd { get; private set; }
        public TypeActivityBuild TypeActivityBuild { get; private set; }
        public bool IsAlive { get; private set; }
    }
}
