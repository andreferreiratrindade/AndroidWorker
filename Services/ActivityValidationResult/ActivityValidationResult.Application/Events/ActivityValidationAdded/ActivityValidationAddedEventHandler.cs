using Framework.Shared.IntegrationEvent.Integration;
using MassTransit;
using MediatR;
using ActivityValidationResult.Domain.DomainEvents;
using Framework.MessageBus;

namespace ActivityValidationResult.Application.Events
{
    public class ActivityValidationAddedEventHandler : INotificationHandler<ActivityValidationResultAddedEvent>
    {
               private readonly IMessageBus _messageBus;


        public ActivityValidationAddedEventHandler(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }
        public async Task Handle(ActivityValidationResultAddedEvent @event, CancellationToken cancellationToken)
        {
            await _messageBus.PublishAsync(new ActivityValidationResultCreatedIntegrationEvent(
                                                @event.ActivityValidationResultId,
                                               @event.ActivityId,
                                               @event.Workers,
                                               @event.TypeActivityBuild,
                                               @event.TimeActivityStart,
                                               @event.TimeActivityEnd,
                                               @event.CorrelationId), cancellationToken);

        }
    }
}
