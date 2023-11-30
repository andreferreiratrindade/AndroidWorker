using MediatR;
using Activities.Domain.DomainEvents;
using Framework.Shared.IntegrationEvent.Integration;
using MassTransit;

namespace Activities.Application.Events
{
    public class ActivityCreatedEventHandler : INotificationHandler<ActivityCreatedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public ActivityCreatedEventHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(ActivityCreatedEvent message, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish(
                       new ActivityCreatedIntegrationEvent(message.ActivityId,
                                                      message.Workers,
                                                      message.TypeActivityBuild.GetHashCode(),
                                                      message.TimeActivityStart,
                                                      message.TimeActivityEnd,
                                                      message.ActivityId));
        }
    }
}