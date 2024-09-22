using System;
using System.Collections.Generic;

using Framework.Core.DomainObjects;
using MassTransit;

namespace Activities.Domain.DomainEvents
{
    public class ActivityCreatedCompensationEvent : RollBackEvent
    {
        public ActivityCreatedCompensationEvent(CorrelationIdGuid correlationId):base(correlationId)
        {
        }
    }
}
