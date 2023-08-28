using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Message.Bus.ExangesAttributes
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExchangeNameAttribute2 : Attribute
    {
        public ExchangeNameAttribute2(string exchangeName)
        {
            ExchangeName = exchangeName;
        }

        public string ExchangeName { get; set; }
    }
}