using Framework.Shared.IntegrationEvent.Integration;
using MassTransit;
using MediatR;
using ActivityValidationResult.Domain.DomainEvents;

namespace ActivityValidationResult.Application.Events
{
    public class ActivityRejectedEventHandler : INotificationHandler<ActivityRejectedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public ActivityRejectedEventHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        public async Task Handle(ActivityRejectedEvent notification, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish(new ActivityRejectedIntegratedEvent(notification.CorrelationId, notification.ActivityId, notification.DescriptionError), cancellationToken);
        }
    }
}
