using EasyNetQ;

namespace Framework.Shared.IntegrationEvent.Integration
{
    [Queue("ActivityUptatedTimeStartAndTimeEnd", ExchangeName = "ActivityExchange")]

    public class ActivityUptatedTimeStartAndTimeEndIntegrationEvent : Framework.Core.Messages.Integration.IntegrationEvent
    {
        public Guid ActivityId { get; private set; }
        public DateTime TimeActivityStart { get; private set; }
        public DateTime TimeActivityEnd { get; private set; }

        public ActivityUptatedTimeStartAndTimeEndIntegrationEvent(Guid activityId,
                                    DateTime timeActivityStart,
                                    DateTime timeActivityEnd)
        {
            this.ActivityId = activityId;
            this.TimeActivityStart = timeActivityStart;
            this.TimeActivityEnd = timeActivityEnd;
        }

    }
}