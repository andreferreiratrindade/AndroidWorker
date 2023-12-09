using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Core.Messages;
using Activities.Domain.Enums;
using Activities.Domain.DTO;
using Framework.Core.DomainObjects;
using MassTransit;

namespace Activities.Domain.DomainEvents
{
    public class ActivityNotCreatedEvent : RollBackEvent
    {
        public ActivityNotCreatedEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }
    }
}