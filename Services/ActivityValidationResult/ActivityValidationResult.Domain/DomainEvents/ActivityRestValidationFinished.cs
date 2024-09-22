using Framework.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityValidationResult.Domain.DomainEvents
{
    public class ActivityRestValidationFinished : DomainEvent
    {
        public Guid ActivityId { get;}

        public ActivityRestValidationFinished(Guid activityId, CorrelationIdGuid correlationId) : base(correlationId)
        {
            this.ActivityId = activityId;
        }
    }
}
