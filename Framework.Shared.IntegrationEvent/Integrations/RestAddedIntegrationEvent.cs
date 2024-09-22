using EasyNetQ;
using Framework.Core.DomainObjects;
using Framework.Core.Messages.Integration;
namespace Framework.Shared.IntegrationEvent.Integration
{

    [Queue("RestAdded", ExchangeName = "RestExchange")]

    public class RestAddedIntegrationEvent : Framework.Core.Messages.Integration.IntegrationEvent
    {
        public RestAddedIntegrationEvent(Guid restId,
                                                Guid activityId,
                                                string workerId,
                                                DateTime timeRestStart,
                                                DateTime timeRestEnd, CorrelationIdGuid correlationId): base(correlationId)
        {
            RestId = restId;
            ActivityId = activityId;
            TimeRestStart = timeRestStart;
            TimeRestEnd = timeRestEnd;
            WorkerId = workerId;
        }

        public Guid ActivityId { get; private set; }
        public Guid RestId { get; private set; }
        public string WorkerId { get; private set; }
        public DateTime TimeRestStart { get; private set; }
        public DateTime TimeRestEnd { get; private set; }
    }
}
