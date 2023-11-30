using MediatR;
using Activities.Domain.DomainEvents;
using Activities.Application.IntegrationEvents;
using Framework.MessageBus;
using Framework.Shared.IntegrationEvent.Integration;
using MassTransit;

namespace Activities.Application.Events
{
    public class ActivityUptatedTimeStartAndTimeEndEventHandler : INotificationHandler<ActivityUptatedTimeStartAndTimeEndEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public ActivityUptatedTimeStartAndTimeEndEventHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(ActivityUptatedTimeStartAndTimeEndEvent message, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish(
                       new ActivityUptatedTimeStartAndTimeEndIntegrationEvent(message.ActivityId,
                                                      message.TimeActivityStart,
                                                      message.TimeActivityEnd));
        }
    }
}