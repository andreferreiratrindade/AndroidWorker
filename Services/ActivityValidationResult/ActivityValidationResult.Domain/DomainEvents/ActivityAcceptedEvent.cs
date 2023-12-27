using ActivityValidationResult.Domain.Enums;
using Framework.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityValidationResult.Domain.DomainEvents
{
    public class ActivityAcceptedEvent : DomainEvent
    {
        public Guid ActivityId { get; set; }
        public TypeStatus TypeStatus { get; set; }

        public ActivityAcceptedEvent(Guid activityId,TypeStatus typeStatus)
        {
            this.TypeStatus = typeStatus;
            CorrelationId = activityId;
            ActivityId = activityId;
        }
    }
}
