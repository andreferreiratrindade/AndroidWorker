using EasyNetQ;
using Framework.Core.Messages.Integration;
namespace Framework.Shared.IntegrationEvent.Integration
{

    [Queue("AddRest", ExchangeName = "RestExchange")]

    public class RestAddedRequestIntegrationEvent : Framework.Core.Messages.Integration.IntegrationEvent
    {
        public RestAddedRequestIntegrationEvent(Guid activityId,
                                                List<string> workers,
                                                int typeActivityBuild,
                                                DateTime timeActivityStart,
                                                DateTime timeRestStart,
                                                Guid correlationId)
        {
            ActivityId = activityId;
            CorrelationId = correlationId;
            Workers = workers;
            TypeActivityBuild = typeActivityBuild;
            TimeRestStart = timeRestStart;
            TimeActivityStart = timeActivityStart;
        }

        public Guid ActivityId { get; private set; }
        public List<string> Workers { get; private set; }
        public int TypeActivityBuild { get; private set; }
        public DateTime TimeRestStart { get; private set; }
        public DateTime TimeActivityStart { get; private set; }
        public Guid CorrelationById { get { return this.CorrelationId; }  }
    }
}