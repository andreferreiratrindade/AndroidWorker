using EasyNetQ;
using Framework.Core.Messages.Integration;
namespace Framework.Shared.IntegrationEvent.Integration
{

    [Queue("ActivityConfirmed", ExchangeName = "ActivityConfirmed")]

    public class ActivityConfirmedIntegrationEvent : Framework.Core.Messages.Integration.IntegrationEvent
    {
       public ActivityConfirmedIntegrationEvent(Guid activityId,
                                               List<string> workers,
                                               int typeActivityBuild,
                                               DateTime timeActivityStart,
                                               DateTime timeActivityEnd,
                                               Guid correlationId
           )
        {
            CorrelationId = correlationId;
            ActivityId = activityId;
            Workers = workers;
            TypeActivityBuild = typeActivityBuild;
            TimeActivityStart = timeActivityStart;
            TimeActivityEnd = timeActivityEnd;
        }

        public Guid ActivityId { get; private set; }
        public List<string> Workers { get; private set; }
        public int TypeActivityBuild { get; private set; }
        public DateTime TimeActivityStart { get; private set; }
        public DateTime TimeActivityEnd { get; private set; }
    }
}
