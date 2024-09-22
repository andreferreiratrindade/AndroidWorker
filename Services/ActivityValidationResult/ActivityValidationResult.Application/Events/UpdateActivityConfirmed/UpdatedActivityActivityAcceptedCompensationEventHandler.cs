using Framework.Shared.IntegrationEvent.Integration;
using MassTransit;
using MediatR;
using ActivityValidationResult.Domain.DomainEvents;

namespace ActivityValidationResult.Application.Events
{
    public class UpdatedActivityActivityAcceptedCompensationEventHandler : INotificationHandler<UpdatedActivityConfirmedCompensationEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public UpdatedActivityActivityAcceptedCompensationEventHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        public async Task Handle(UpdatedActivityConfirmedCompensationEvent @event, CancellationToken cancellationToken)
        {
            //await _publishEndpoint.Publish(new ActivityAcceptedIntegrationEvent(@event.ActivityId), cancellationToken);
            // Task.CompletedTask;
        }
    }
}
