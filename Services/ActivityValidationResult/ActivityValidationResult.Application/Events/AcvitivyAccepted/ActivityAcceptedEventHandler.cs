using Framework.Shared.IntegrationEvent.Integration;
using MassTransit;
using MediatR;
using ActivityValidationResult.Domain.DomainEvents;
using Framework.MessageBus;

namespace ActivityValidationResult.Application.Events
{
    public class ActivityAcceptedEventHandler : INotificationHandler<ActivityAcceptedEvent>
    {
         private readonly IMessageBus _messageBus;

        public ActivityAcceptedEventHandler(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }
        public async Task Handle(ActivityAcceptedEvent @event, CancellationToken cancellationToken)
        {
            await _messageBus.PublishAsync(new ActivityAcceptedIntegrationEvent(@event.ActivityId, @event.CorrelationId), cancellationToken);
        }
    }
}
