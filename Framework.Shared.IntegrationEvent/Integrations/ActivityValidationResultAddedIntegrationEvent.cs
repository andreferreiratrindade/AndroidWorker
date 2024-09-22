using EasyNetQ;
using Framework.Core.DomainObjects;
using Framework.Core.Messages.Integration;
using Framework.Shared.IntegrationEvent.Enums;
namespace Framework.Shared.IntegrationEvent.Integration
{

    [Queue("ActivityValidationResultCreated", ExchangeName = "Activity")]

    public class ActivityValidationResultCreatedIntegrationEvent : Framework.Core.Messages.Integration.IntegrationEvent
    {
       public ActivityValidationResultCreatedIntegrationEvent(
                                                Guid activityValidationResultId,
                                                Guid activityId,
                                                List<string> workers,
                                                TypeActivityBuild typeActivityBuild,
                                                DateTime timeActivityStart,
                                                DateTime timeActivityEnd, CorrelationIdGuid correlationId): base(correlationId)

        {
            AggregateId = activityValidationResultId;
            ActivityValidationResultId = activityValidationResultId;
            ActivityId = activityId;
            Workers = workers;
            TypeActivityBuild = typeActivityBuild;
            TimeActivityStart = timeActivityStart;
            TimeActivityEnd = timeActivityEnd;
        }

        public Guid ActivityId { get; private set; }
        public Guid ActivityValidationResultId { get; }
        public List<string> Workers { get; private set; }
        public TypeActivityBuild TypeActivityBuild { get; private set; }
        public DateTime TimeActivityStart { get; private set; }
        public DateTime TimeActivityEnd { get; private set; }
    }
}
