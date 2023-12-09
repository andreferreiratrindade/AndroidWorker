using Framework.Core.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.DomainObjects
{
    public abstract class RollBackEvent : DomainEvent
    {
        public List<NotificationMessage> Notifications { get; } = new List<NotificationMessage>();

        public void AddNotifications(List<NotificationMessage> notifications)
        {
            Notifications.AddRange(notifications);
        }
        void AddNotification(NotificationMessage notifications)
        {
            Notifications.Add(notifications);
        }
    }
}
