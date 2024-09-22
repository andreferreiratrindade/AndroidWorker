using Framework.Shared.IntegrationEvent.Integration;
using MassTransit;
using MediatR;
using ActivityValidationResult.Domain.DomainEvents;
using Framework.MessageBus;

namespace ActivityValidationResult.Application.Events
{
    public class ActivityRejectedEventHandler : INotificationHandler<ActivityRejectedEvent>
    {
               private readonly IMessageBus _messageBus;


        public ActivityRejectedEventHandler(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }
        public async Task Handle(ActivityRejectedEvent @event, CancellationToken cancellationToken)
        {
            await _messageBus.PublishAsync(new ActivityRejectedIntegrationEvent(
                                               @event.ActivityId,
                                               @event.DescriptionError,
                                               @event.CorrelationId), cancellationToken);

        }
    }
}
