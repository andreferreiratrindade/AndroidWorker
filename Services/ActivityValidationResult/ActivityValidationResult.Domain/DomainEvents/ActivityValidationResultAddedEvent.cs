using ActivityValidationResult.Domain.Enums;
using Framework.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityValidationResult.Domain.DomainEvents
{
    public class ActivityValidationResultAddedEvent : DomainEvent
    {
        public Guid ActivityId { get; set; }
        public TypeStatus TypeStatus { get; }
        public Guid ActivityValidationResultId { get; set; }

        public ActivityValidationResultAddedEvent(Guid activityValidationResultId, Guid activityId, Enums.TypeStatus typeStatus, Guid correlationId)
        {
            CorrelationId = correlationId;
            AggregateId = activityValidationResultId;
            ActivityId = activityId;
            TypeStatus = typeStatus;
        }
    }
}
