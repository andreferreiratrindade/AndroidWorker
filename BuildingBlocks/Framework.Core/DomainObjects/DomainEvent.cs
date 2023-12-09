using Framework.Core.EventSourcingUtils;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.DomainObjects
{
    public abstract class DomainEvent : EventSourcingNotification, IDomainEvent, CorrelatedBy<Guid>
    {
        public Guid EventId { get; private set; }
        public Guid AggregateId { get; protected set; }
        public long AggregateVersion { get; set; }
        public DateTime TimeStamp { get; }

        public Guid CorrelationId { get; protected set; }

        protected DomainEvent()
        {
            EventId = Guid.NewGuid();
            TimeStamp = DateTime.UtcNow;
        }

        protected DomainEvent(Guid aggregateId) : this()
        {
            AggregateId = aggregateId;
        }

        protected DomainEvent(Guid aggregateId, long aggregateVersion) : this(aggregateId)
        {
            AggregateVersion = aggregateVersion;
        }
    }
}
