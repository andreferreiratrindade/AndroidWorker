using MediatR;
using Activities.Domain.DomainEvents;
using Activities.Application.IntegrationEvents;
using Framework.MessageBus;
using Framework.Shared.IntegrationEvent.Integration;

namespace Activities.Application.Events
{
    public class ActivityUptatedTimeStartAndTimeEndEventHandler : INotificationHandler<ActivityUptatedTimeStartAndTimeEndEvent>
    {
        private readonly IMessageBus _bus;
        public ActivityUptatedTimeStartAndTimeEndEventHandler(IMessageBus bus)
        {
            this._bus = bus;
        }

        public async Task Handle(ActivityUptatedTimeStartAndTimeEndEvent message, CancellationToken cancellationToken)
        {
            await _bus.PublishAsync(
                       new ActivityUptatedTimeStartAndTimeEndIntegrationEvent(message.ActivityId,
                                                      message.TimeActivityStart,
                                                      message.TimeActivityEnd));
        }
    }
}