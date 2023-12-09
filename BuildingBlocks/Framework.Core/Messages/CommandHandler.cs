using System.Diagnostics.Tracing;
using System.Threading.Tasks;
using FluentValidation.Results;
using Framework.Core.Data;
using Framework.Core.DomainObjects;
using Framework.Core.Mediator;
using Framework.Core.Notifications;
using MediatR;
using SharpCompress.Common;

namespace Framework.Core.Messages
{
    public abstract class CommandHandler
    {
        protected readonly IDomainNotification _domainNotification;
        protected readonly IMediatorHandler _mediatorHandler;
        public CommandHandler(IDomainNotification domainNotification, IMediatorHandler mediatorHandler)
        {
            _domainNotification = domainNotification;
            _mediatorHandler = mediatorHandler;
        }

        protected async Task PersistData(IUnitOfWork uow)
        {
            if (!_domainNotification.HasNotifications)
            {
                if (!await uow.Commit()) _domainNotification.AddNotifications(string.Empty, "Error connection database");
            }
        }

        protected async Task RollBackEvent(RollBackEvent @event)
        {
            if (_domainNotification.HasNotifications)
            {
                @event.AddNotifications(_domainNotification.Notifications.ToList());
                await _mediatorHandler.PublishEvent(@event);
            }
        }

        protected async Task PersistDataOrRollBackEvent(IUnitOfWork uow, RollBackEvent @event)
        {
            await PersistData(uow);

            await RollBackEvent(@event);
        }
    }
}