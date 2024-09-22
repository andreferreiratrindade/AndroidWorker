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
        public RollBackEvent(CorrelationIdGuid correlationId):base(correlationId)
        {

        }
        public List<NotificationMessage> Notifications { get; } = new List<NotificationMessage>();

        public List<string> NotificationsToString => Notifications.Select(x => x.Value).ToList();

        public void AddNotification(List<NotificationMessage> notifications)
        {
            Notifications.AddRange(notifications);
        }
        public void AddNotification(NotificationMessage notifications)
        {
            Notifications.Add(notifications);
        }


        public void AddNotification(List<string> message){
            message.ForEach(x=> AddNotification(x));
        }
        public void AddNotification(string message){
            Notifications.Add(new NotificationMessage("", message));
        }
    }
}
