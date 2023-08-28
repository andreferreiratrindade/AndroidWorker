using System;

namespace Framework.Core.DomainObjects
{
    public class InstructionException : Exception
    {
        public List<string> Messagens{get;set;}

        public void AddMessage(string message){
            Messagens ??= new List<string>();
            Messagens.Add(message);
        }
         public InstructionException(List<string> messagens): base(string.Join(';',messagens))
        { Messagens = messagens;}

        public InstructionException(string message) : base(message)
        { AddMessage(message); }

        public InstructionException(string message, Exception innerException) : base(message, innerException)
        {  AddMessage(message);}

    }
}