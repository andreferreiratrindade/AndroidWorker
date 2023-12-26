using EasyNetQ;
using Framework.Core.Messages.Integration;
namespace Framework.Shared.IntegrationEvent.Integration
{

    [Queue("ActivityValidationResultAdded", ExchangeName = "Activity")]

    public class ActivityValidationResultAddedIntegrationEvent : Framework.Core.Messages.Integration.IntegrationEvent
    {
       public ActivityValidationResultAddedIntegrationEvent(Guid activityValidationResultId,
           Guid activityId,
                                               Guid correlationId
           )
        {
            AggregateId = activityValidationResultId;
            ActivityValidationResultId = activityValidationResultId;
            CorrelationId = correlationId;
            ActivityId = activityId;
        }

        public Guid ActivityId { get; private set; }
        public Guid ActivityValidationResultId { get; }
    }
}