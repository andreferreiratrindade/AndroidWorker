using System.Runtime.InteropServices;
using Framework.Core.DomainObjects;
using Worker.Domain.DomainEvents;

namespace Worker.Domain.Models.Entities
{
    public class WorkerEntity : AggregateRoot, IAggregateRoot
    {

        protected WorkerEntity()
        {

        }
        private WorkerEntity(string workerId)
        {
            var @event = new WorkerAddedEvent(workerId);
            RaiseEvent(@event);
        }

        public string WorkerId { get; private set; }

        protected override void When(IDomainEvent @event)
        {
            switch (@event)
            {
                case WorkerAddedEvent x: OnWorkerAddedEvent(x); break;
            }

        }


        public static WorkerEntity Create(string workerId){
            var workerEntity =  new WorkerEntity(workerId);
            return workerEntity;
        }

        private void OnWorkerAddedEvent(WorkerAddedEvent @event){
            this.WorkerId = @event.WorkerId;
        }
    }
}
