using Framework.MessageBus;
using Framework.Shared.IntegrationEvent.Integration;
using MassTransit;
using MediatR;
using Rests.Domain.DomainEvents;

namespace Rests.Application.Events
{
    public class RestAddedEventHandler : INotificationHandler<RestAddedEvent>
    {
        private readonly IMessageBus _messageBus;

        public RestAddedEventHandler(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }
        public async Task Handle(RestAddedEvent notification, CancellationToken cancellationToken)
        {
            await _messageBus.PublishAsync(new RestAddedIntegrationEvent(notification.RestId,
                                                                         notification.ActivityId,
                                                                         notification.WorkerId,
                                                                         notification.TimeRestStart,
                                                                         notification.TimeRestEnd,
                                                                         notification.CorrelationId),cancellationToken);
        }
    }
}
