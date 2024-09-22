using Framework.Shared.IntegrationEvent.Integration;
using MassTransit;
using MediatR;
using Rests.Domain.DomainEvents;

namespace Rests.Application.Events
{
    public class RestAddedCompensationHandler : INotificationHandler<RestAddedCompensationEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public RestAddedCompensationHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        public async Task Handle(RestAddedCompensationEvent @event, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish(new RestRejectedIntegrationEvent(@event.ActivityId, @event.WorkerId,
            @event.NotificationsToString, @event.CorrelationId), cancellationToken);
        }
    }
}
