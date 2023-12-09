using Framework.Shared.IntegrationEvent.Integration;
using MassTransit;
using MediatR;
using Rests.Domain.DomainEvents;

namespace Rests.Application.Events
{
    public class RestAddedEventHandler : INotificationHandler<RestAddedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public RestAddedEventHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        public async Task Handle(RestAddedEvent notification, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish(new RestAddedIntegrationEvent(notification.RestId,
                                                                         notification.ActivityId,
                                                                         notification.WorkerId,
                                                                         notification.TimeRestStart,
                                                                         notification.TimeRestEnd,
                                                                         notification.CorrelationId), cancellationToken);
        }
    }
}
