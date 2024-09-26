using FluentValidation;
using FluentValidation.Results;
using Framework.Core.Data;
using Framework.Core.DomainObjects;
using Framework.Core.Mediator;
using Framework.Core.Notifications;
using MediatR;
using SharpCompress.Readers;

namespace Framework.Core.Messages
{
    public abstract class CommandHandler<TCommand, TOutput, TValidation> : IRequestHandler<TCommand, TOutput>
        where TCommand : Command<TOutput>
        where TValidation : AbstractValidator<TCommand>

    {
        protected readonly IDomainNotification _domainNotification;
        protected readonly IMediatorHandler _mediatorHandler;
        public CommandHandler(IDomainNotification domainNotification, IMediatorHandler mediatorHandler)
        {
            _domainNotification = domainNotification;
            _mediatorHandler = mediatorHandler;
        }

        protected CommandHandler()
        {
        }

        public async Task<TOutput> Handle(TCommand request, CancellationToken cancellationToken)
        {
           _domainNotification.AddNotification(ExecuteValidations(request));
           if(_domainNotification.HasNotifications) return request.GetCommandOutput();

            var resultCommand = await ExecutCommand(request, cancellationToken);

           return resultCommand;
        }

        private  ValidationResult ExecuteValidations(TCommand request){
           var validation =  Activator.CreateInstance<TValidation>();
           var result  = validation.Validate(request);
           return result;

        }
        public abstract Task<TOutput> ExecutCommand(TCommand request, CancellationToken cancellationToken);

        protected async Task PersistData(IUnitOfWork uow)
        {
            if (!_domainNotification.HasNotifications)
            {
                if (!await uow.Commit()) _domainNotification.AddNotification(string.Empty, "Error connection database");
            }
        }

        protected async Task PublishEvents(IAggregateRoot aggregateRoot)
        {
            if (!_domainNotification.HasNotifications)
            {

                await _mediatorHandler.PublishEvent(aggregateRoot.GetUncommittedChanges());

                aggregateRoot.MarkChangesAsCommitted();
            }
        }


    }
}
