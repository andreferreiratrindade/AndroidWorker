using Framework.Shared.IntegrationEvent.Integration;
using MassTransit;
using MediatR;
using Rests.Domain.DomainEvents;

namespace Rests.Application.Events
{
    public class RestNotAddedEventHandler : INotificationHandler<RestNotAddedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public RestNotAddedEventHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        public async Task Handle(RestNotAddedEvent notification, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish(new RestNotAddedIntegrationEvent(notification.CorrelationId, notification.Notifications), cancellationToken);
        }
    }
}
