using MediatR;
using Activities.Domain.DomainEvents;
using Framework.Shared.IntegrationEvent.Integration;
using MassTransit;
using Framework.MessageBus;

namespace Activities.Application.Events
{
    public class ActivityCreatedEventHandler : INotificationHandler<ActivityCreatedEvent>
    {
        private readonly IMessageBus _messageBus;

        public ActivityCreatedEventHandler(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public async Task Handle(ActivityCreatedEvent message, CancellationToken cancellationToken)
        {
                  await _messageBus.PublishAsync(

                       new ActivityCreatedIntegrationEvent(message.ActivityId,
                                                      message.Workers,
                                                      message.TypeActivityBuild.GetHashCode(),
                                                      message.TimeActivityStart,
                                                      message.TimeActivityEnd,
                                                      message.CorrelationId),cancellationToken);
        }
    }
}
