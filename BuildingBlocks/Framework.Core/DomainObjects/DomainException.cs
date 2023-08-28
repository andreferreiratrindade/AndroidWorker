using System;

namespace Framework.Core.DomainObjects
{
    public class DomainException : Exception
    {
        public List<string> Messagens{get;set;}

        public void AddMessage(string message){
            Messagens ??= new List<string>();
            Messagens.Add(message);
        }
         public DomainException(List<string> messagens): base(string.Join(';',messagens))
        { Messagens = messagens;}

        public DomainException(string message) : base(message)
        { AddMessage(message); }

        public DomainException(string message, Exception innerException) : base(message, innerException)
        {  AddMessage(message);}

    }
}