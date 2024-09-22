using Framework.Core.DomainObjects;
using ZstdSharp.Unsafe;

namespace Worker.Domain.DomainEvents
{
    public  sealed class WorkerAddedEvent : DomainEvent
    {
        public WorkerAddedEvent( string workerId, CorrelationIdGuid correlationId): base(correlationId)
        {
            WorkerId = workerId;
        }
        public string WorkerId { get; set; }
    }

    public sealed class WorkerAddCompensationEvent:RollBackEvent{
        public WorkerAddCompensationEvent(List<string> workersId, CorrelationIdGuid correlationId): base(correlationId){
            WorkersId = workersId;
        }
        public List<string> WorkersId { get; set; }
    }
}
