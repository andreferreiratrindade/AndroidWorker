using Framework.Core.DomainObjects;
using ActivityValidationResult.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityValidationResult.Domain.DomainEvents
{
    public class ActivityAcceptedCompensationEvent : RollBackEvent
    {
        public Guid ActivityId { get; set; }
        public ActivityAcceptedCompensationEvent(Guid activityId, CorrelationIdGuid correlationId) : base(correlationId)
        {

            ActivityId = activityId;
        }
    }
}
