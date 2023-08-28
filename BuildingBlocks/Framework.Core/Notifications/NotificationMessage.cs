using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Notifications
{
    public class NotificationMessage
    {
        public NotificationMessage(string key, string value)
        {
            Id = Guid.NewGuid();
            Key = key;
            Value = value;
        }

        public Guid Id { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
    }
}
