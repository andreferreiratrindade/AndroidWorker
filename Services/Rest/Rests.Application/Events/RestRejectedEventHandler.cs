using Framework.Shared.IntegrationEvent.Integration;
using MassTransit;
using MediatR;
using Rests.Domain.DomainEvents;

namespace Rests.Application.Events
{
    public class RestRejectedEventHandler : INotificationHandler<RestRejectedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public RestRejectedEventHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        public async Task Handle(RestRejectedEvent @event, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish(new RestRejectedIntegrationEvent(@event.CorrelationId, @event.Notifications), cancellationToken);
        }
    }
}
