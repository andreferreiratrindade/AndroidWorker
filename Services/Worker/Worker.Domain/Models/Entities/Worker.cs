using Framework.Core.DomainObjects;

namespace Worker.Domain.Models.Entities
{
    public class Worker : Entity, IAggregateRoot
    {

        protected Worker()
        {
            
        }
        public Worker(string workerId)
        {
            this.WorkerId = workerId;
        }
        public string WorkerId { get; private set; }
       
    }
}