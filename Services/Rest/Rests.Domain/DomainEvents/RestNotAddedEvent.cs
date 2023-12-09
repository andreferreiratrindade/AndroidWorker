using Framework.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rests.Domain.DomainEvents
{
    public class RestNotAddedEvent : RollBackEvent
    {

        public RestNotAddedEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }
    }
}
