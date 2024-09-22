using MediatR;
using Activities.Domain.DomainEvents;
using Framework.Shared.IntegrationEvent.Integration;
using MassTransit;
using Framework.MessageBus;

namespace Activities.Application.Events
{
    public class ActivityConfirmedEventHandler : INotificationHandler<ActivityConfirmedEvent>
    {
        private readonly IMessageBus _messageBus;
        public ActivityConfirmedEventHandler(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public async Task Handle(ActivityConfirmedEvent message, CancellationToken cancellationToken)
        {
            await _messageBus.PublishAsync(
                       new ActivityConfirmedIntegrationEvent(message.ActivityId,
                                                     message.Workers ,
                                                      message.TypeActivityBuild.GetHashCode(),
                                                      message.TimeActivityStart,
                                                      message.TimeActivityEnd,
                                                      message.CorrelationId),cancellationToken);
        }
    }
}
