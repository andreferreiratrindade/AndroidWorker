using ActivityValidationResult.Domain.Enums;
using Framework.Core.DomainObjects;
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
        public TypeStatus TypeStatus { get; set; }
        public List<string> DescriptionError {get;set;}

       public ActivityRejectedEvent(Guid activityId, TypeStatus typeStatus, List<string> descriptionError, CorrelationIdGuid correlationId) : base(correlationId)
        {
            this.DescriptionError = descriptionError;
            this.TypeStatus = typeStatus;
            ActivityId = activityId;
        }
    }
}
