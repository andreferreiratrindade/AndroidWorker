using ActivityValidationResult.Domain.Enums;
using Framework.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityValidationResult.Domain.DomainEvents
{
    public class UpdatedActivityConfirmedCompensationEvent : RollBackEvent
    {
        public Guid ActivityId { get; set; }


        public UpdatedActivityConfirmedCompensationEvent(Guid activityId, CorrelationIdGuid correlationId) : base(correlationId)
        {

            ActivityId = activityId;
        }
    }
}
