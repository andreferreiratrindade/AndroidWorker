using System;

namespace Framework.Message.Bus.ExangesAttributes
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class QueueNameAttribute : Attribute
    {
        public QueueNameAttribute(string queueName)
        {
            QueueName = queueName;
        }

        public string QueueName { get; set; }
    }
}