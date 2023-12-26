using Framework.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityValidationResult.Domain.DomainEvents
{
    public class WorkerActivityCreatedEvent : DomainEvent
    {
        public string WorkerId { get; }

        public WorkerActivityCreatedEvent( string workerId, Guid correlationId)
        {
            CorrelationId = correlationId;
            WorkerId = workerId;
        }
    }
}
