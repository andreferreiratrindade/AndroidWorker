using EasyNetQ;
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
                                                DateTime timeRestEnd,
                                                Guid correlationId)
        {
            RestId = restId;
            ActivityId = activityId;
            CorrelationId = correlationId;
            TimeRestStart = timeRestStart;
            TimeRestEnd = timeRestEnd;
            WorkerId = workerId;
        }

        public Guid ActivityId { get; private set; }
        public Guid RestId { get; private set; }
        public string WorkerId { get; private set; }
        public DateTime TimeRestStart { get; private set; }
        public DateTime TimeRestEnd { get; private set; }
        public Guid CorrelationById { get { return this.CorrelationId; }  }
    }
}