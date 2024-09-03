using MediatR;
using Activities.Domain.DomainEvents;
using Framework.Shared.IntegrationEvent.Integration;
using MassTransit;
using MassTransit.Audit;

namespace Activities.Application.Events
{
    public class ActivityConfirmedCompensationEventHandler : INotificationHandler<ActivityConfirmedCompensationEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public ActivityConfirmedCompensationEventHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(ActivityConfirmedCompensationEvent message, CancellationToken cancellationToken)
        {
            // await _publishEndpoint.Publish(
            //            new ActivityConfirmedCompensationIntegrationEvent(message.CorrelationId, message.Notifications));
            await Task.CompletedTask;
        }
    }
}
