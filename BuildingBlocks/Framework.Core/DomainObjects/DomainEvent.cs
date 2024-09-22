using Framework.Core.EventSourcingUtils;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.DomainObjects
{
    public abstract class DomainEvent : EventSourcingNotification, IDomainEvent
    {
        public Guid EventId { get; private set; }
        public Guid AggregateId { get; protected set; }
        public long AggregateVersion { get; set; }
        public DateTime TimeStamp { get; }

        public CorrelationIdGuid CorrelationId { get; }

        private DomainEvent()
        {
            EventId = Guid.NewGuid();
            TimeStamp = DateTime.UtcNow;
        }
        protected DomainEvent(CorrelationIdGuid CorrelationId):this(){
            this.CorrelationId = CorrelationId;
        }

        // protected DomainEvent(Guid aggregateId) : this()
        // {
        //     AggregateId = aggregateId;
        // }

        // protected DomainEvent(Guid aggregateId, long aggregateVersion) : this(aggregateId)
        // {
        //     AggregateVersion = aggregateVersion;
        // }
    }
}
