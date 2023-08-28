using FluentValidation.Results;
using Framework.Core.DomainObjects;
using MediatR;

namespace Framework.Core.Messages
{

    public abstract class  Command<TResult> :Message, ICommand<TResult>
    {
        public DateTime Timestamp { get; private set; }
        protected Command()
        {
            Timestamp = DateTime.Now;
        }


        public void ValidCommand(ValidationResult validationResult)
        {
           if(!validationResult.IsValid) throw new InstructionException(validationResult.Errors.Select(x=> x.ErrorMessage).ToList());
        }
    }

}