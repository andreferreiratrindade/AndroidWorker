using ActivityValidationResult.Domain.Enums;
using Framework.Core.DomainObjects;
using Framework.Shared.IntegrationEvent.Enums;
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
        public TypeActivityBuild TypeActivityBuild { get; private set; }
        public DateTime TimeActivityStart { get; private set; }
        public DateTime TimeActivityEnd { get; private set; }
        public List<string> Workers {get; private set;}


        public ActivityValidationResultAddedEvent(
            Guid activityValidationResultId,
            Guid activityId,
            TypeActivityBuild typeActivityBuild,
            DateTime timeActivityStart,
            DateTime timeActivityEnd ,
            Enums.TypeStatus typeStatus,
            List<string> workers, CorrelationIdGuid correlationId):base(correlationId)
        {
            AggregateId = activityValidationResultId;
            ActivityId = activityId;
            TypeStatus = typeStatus;
            TypeActivityBuild =typeActivityBuild;
            TimeActivityEnd = timeActivityEnd;
            TimeActivityStart = timeActivityStart;
            Workers = workers;

        }
    }
}
