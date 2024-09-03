using MassTransit;
using MediatR;
using Worker.Domain.DomainEvents;

namespace Worker.Application.Events
{
    public class WorkerAddedEventHandler(IPublishEndpoint publishEndpoint) : INotificationHandler<WorkerAddedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task Handle(WorkerAddedEvent @event, CancellationToken cancellationToken)
        {
          //  await _publishEndpoint.Publish(new ActivityAcceptedIntegrationEvent(@event.ActivityId), cancellationToken);
        }
    }
}
