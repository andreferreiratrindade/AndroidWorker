using MediatR;
using Activities.Domain.DomainEvents;
using Framework.MessageBus;
using Framework.Shared.IntegrationEvent.Integration;

namespace Activities.Application.Events
{
    public class ActivityCreatedEventHandler : INotificationHandler<ActivityCreatedEvent>
    {
        private readonly IMessageBus _bus;
         public ActivityCreatedEventHandler(IMessageBus bus)
         {
            this._bus = bus;
         }

        public async Task Handle(ActivityCreatedEvent message, CancellationToken cancellationToken)
        {
             await   _bus.PublishAsync( 
                        new ActivityCreatedIntegrationEvent(message.ActivityId,
                                                       message.Workers,
                                                       message.TypeActivityBuild.GetHashCode(),
                                                       message.TimeActivityStart,
                                                       message.TimeActivityEnd));
        }
    }
}