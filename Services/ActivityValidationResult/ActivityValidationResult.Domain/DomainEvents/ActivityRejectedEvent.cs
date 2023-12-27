using Framework.Core.DomainObjects;
using ActivityValidationResult.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityValidationResult.Domain.DomainEvents
{
    public class ActivityRejectedEvent : DomainEvent
    {
        public Guid ActivityId { get; set; }
        public TypeStatus TypeStatus { get; }
        public List<string> DescriptionError { get; set; }
        public ActivityRejectedEvent(Guid activityId, TypeStatus typeStatus, List<string> descriptionError)
        {
            this.DescriptionError = descriptionError;
            this.TypeStatus = typeStatus;
            ActivityId = activityId;

            CorrelationId = activityId;
        }
    }
}