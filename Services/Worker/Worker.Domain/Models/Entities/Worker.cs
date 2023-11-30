using Framework.Core.DomainObjects;

namespace Worker.Domain.Models.Entities
{
    public class Worker : AggregateRoot, IAggregateRoot
    {

        protected Worker()
        {
            
        }
        public Worker(string workerId)
        {
            this.WorkerId = workerId;
        }
        public string WorkerId { get; private set; }

        protected override void When(IDomainEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}