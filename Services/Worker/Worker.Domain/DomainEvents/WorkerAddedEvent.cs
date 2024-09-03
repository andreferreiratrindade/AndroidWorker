using Framework.Core.DomainObjects;

namespace Worker.Domain.DomainEvents
{
    public  sealed class WorkerAddedEvent(string workerId) : DomainEvent
    {
        public string WorkerId { get; set; } = workerId;
    }
}
