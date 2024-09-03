using MediatR;
using Activities.Domain.DomainEvents;
using Framework.Shared.IntegrationEvent.Integration;
using MassTransit;

namespace Activities.Application.Events
{
    public class ActivityCreatedCompensationEventHandler : INotificationHandler<ActivityCreatedCompensationEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public ActivityCreatedCompensationEventHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

         public async Task Handle(ActivityCreatedCompensationEvent message, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish(
                       new ActivityConfirmedCompensationIntegrationEvent(message.CorrelationId, message.Notifications));
        }
    }
}
