using Framework.Shared.IntegrationEvent.Integration;
using MassTransit;
using MediatR;
using ActivityValidationResult.Domain.DomainEvents;

namespace ActivityValidationResult.Application.Events
{
    public class ActivityValidatioAddedCompensationEventHandler : INotificationHandler<ActivityValidationResultAddedCompensationEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public ActivityValidatioAddedCompensationEventHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        public async Task Handle(ActivityValidationResultAddedCompensationEvent @event, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish(new ActivityAcceptedIntegrationEvent(@event.ActivityId, @event.CorrelationId), cancellationToken);
        }
    }
}
