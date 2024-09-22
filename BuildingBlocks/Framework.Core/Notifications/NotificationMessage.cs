using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit.Contracts;

namespace Framework.Core.Notifications
{
    public class NotificationMessage
    {
        public NotificationMessage(string key, string value, NotificationMessageType notificationMessageType = NotificationMessageType.Rule)
        {
            Id = Guid.NewGuid();
            Key = key;
            Value = value;
            NotificationMessageType = notificationMessageType;
        }

        public Guid Id { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public NotificationMessageType NotificationMessageType {get; private set;}
    }

    public enum NotificationMessageType
    {
        Rule = 1,
        Excetpion = 2
    }
}
