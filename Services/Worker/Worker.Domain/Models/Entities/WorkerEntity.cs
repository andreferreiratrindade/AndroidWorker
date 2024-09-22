using System.Runtime.InteropServices;
using Framework.Core.DomainObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Worker.Domain.DomainEvents;

namespace Worker.Domain.Models.Entities
{
    public class WorkerEntity : AggregateRoot, IAggregateRoot
    {

        protected WorkerEntity()
        {

        }
        private WorkerEntity(string workerId, CorrelationIdGuid correlationId)
        {
            var @event = new WorkerAddedEvent(workerId, correlationId);
            RaiseEvent(@event);
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string WorkerId { get; private set; }

        protected override void When(IDomainEvent @event)
        {
            switch (@event)
            {
                case WorkerAddedEvent x: OnWorkerAddedEvent(x); break;
            }

        }


        public static WorkerEntity Create(string workerId,CorrelationIdGuid correlationId){
            var workerEntity =  new WorkerEntity(workerId,correlationId );
            return workerEntity;
        }

        private void OnWorkerAddedEvent(WorkerAddedEvent @event){
            this.WorkerId = @event.WorkerId;
        }
    }
}
