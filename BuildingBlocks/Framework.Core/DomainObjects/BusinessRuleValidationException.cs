using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Core.DomainObjects
{
    public class BusinessRuleValidationException : Exception
    {

        public string Details { get; }

        public BusinessRuleValidationException(string message) : base(message)
        {
            this.Details = message;
        }

        public override string ToString()
        {
            return $"{this.Details}";
        }
    }
}