using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using Newtonsoft.Json;

namespace Framework.Message.Bus.ExangesAttributes
{
    public class AttributeBasedConventions : Conventions
    {
        private readonly ITypeNameSerializer _typeNameSerializer;

        public AttributeBasedConventions(ITypeNameSerializer typeNameSerializer)
            : base(typeNameSerializer)
        {
            if (typeNameSerializer == null)
                throw new ArgumentNullException("typeNameSerializer");

            _typeNameSerializer = typeNameSerializer;

            QueueNamingConvention = GenerateQueueName;
        }

        //private string GenerateExchangeName(Type messageType)
        //{
        //    var exchangeNameAtt = messageType.GetCustomAttributes(typeof(ExchangeNameAttribute), true).SingleOrDefault() as ExchangeNameAttribute;

        //    return (exchangeNameAtt == null)
        //        ? _typeNameSerializer.Serialize(messageType)
        //        : exchangeNameAtt.ExchangeName;
        //}

        private string GenerateQueueName(Type messageType, string subscriptionId)
        {
            var queueNameAtt = messageType.GetCustomAttributes(typeof(QueueNameAttribute), true).SingleOrDefault() as QueueNameAttribute;

            if (queueNameAtt == null)
            {
                var typeName = _typeNameSerializer.Serialize(messageType);
                return string.Format("{0}_{1}", typeName, subscriptionId);
            }

            return string.IsNullOrEmpty(subscriptionId)
                ? queueNameAtt.QueueName
                : string.Concat(queueNameAtt.QueueName, "_", subscriptionId);
        }
    }
}