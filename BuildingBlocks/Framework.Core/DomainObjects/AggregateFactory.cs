using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.DomainObjects
{
    internal static class AggregateFactory<T>
    {
        private static readonly Func<T> _constructor = CreateTypeConstructor();

        private static Func<T> CreateTypeConstructor()
        {
            try
            {
                var newExpr = Expression.New(typeof(T));
                var func = Expression.Lambda<Func<T>>(newExpr);
                return func.Compile();
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        public static T CreateAggregate()
        {
            if (_constructor == null)
                throw new Exception("Not found constructor create aggregate()");
            return _constructor();
        }
    }
}