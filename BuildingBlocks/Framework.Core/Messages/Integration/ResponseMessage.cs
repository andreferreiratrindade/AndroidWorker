using FluentValidation.Results;
using Framework.Core.Notifications;
using MediatR;

namespace Framework.Core.Messages.Integration
{
    public class ResponseMessage : Message
    {
        public IReadOnlyCollection<NotificationMessage> Notifications { get; set; }
        public bool HasNotifications => Notifications.Any();


        public ResponseMessage( IReadOnlyCollection<NotificationMessage> notifications)
        {
            Notifications = notifications;
        }

    }
}