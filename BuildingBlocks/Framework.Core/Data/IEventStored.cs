using Framework.Core.DomainObjects;
using Framework.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Data
{
    public interface IEventStored
    {
        Task<T> Get<T>(Guid aggregateId, CancellationToken cancellationToken = default) where T : AggregateRoot;
        Task SaveAsync(IEnumerable<IDomainEvent> events, Guid aggregateId, string aggregateType);
    }
}
