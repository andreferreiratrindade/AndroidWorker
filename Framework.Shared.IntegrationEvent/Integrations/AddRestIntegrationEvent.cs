using EasyNetQ;
using Framework.Core.Messages.Integration;
namespace Framework.Shared.IntegrationEvent.Integration
{

    [Queue("AddRest", ExchangeName = "RestExchange")]

    public class AddRestIntegrationEvent : Framework.Core.Messages.Integration.IntegrationEvent
    {
       public AddRestIntegrationEvent(Guid activityId,
                                               List<string> workers,
                                               int typeActivityBuild,
                                               DateTime timeActivityStart,
                                               DateTime timeRestStart)
        {
            ActivityId = activityId;
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
    }
}