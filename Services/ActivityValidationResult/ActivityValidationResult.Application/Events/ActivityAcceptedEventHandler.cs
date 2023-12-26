using Framework.Shared.IntegrationEvent.Integration;
using MassTransit;
using MediatR;
using ActivityValidationResult.Domain.DomainEvents;

namespace ActivityValidationResult.Application.Events
{
    public class ActivityAcceptedEventHandler : INotificationHandler<ActivityAcceptedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public ActivityAcceptedEventHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        public async Task Handle(ActivityAcceptedEvent @event, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish(new ActivityAcceptedIntegrationEvent(@event.ActivityId), cancellationToken);
        }
    }
}
