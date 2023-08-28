using MediatR;
using Activities.Domain.DomainEvents;

namespace Activities.Application.Events
{
    public class ActivityInativatedEventHandler : INotificationHandler<ActivityInativatedEvent>
    {
         public ActivityInativatedEventHandler()
         {
         }

        public async Task Handle(ActivityInativatedEvent message, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}