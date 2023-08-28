using System.Threading.Tasks;
using FluentValidation.Results;
using Framework.Core.Data;
using Framework.Core.DomainObjects;
using Framework.Core.Notifications;
using MediatR;

namespace Framework.Core.Messages
{
    public abstract class CommandHandler 
    {
        protected readonly IDomainNotification _domainNotification;
        public CommandHandler(IDomainNotification domainNotification)
        {
            _domainNotification = domainNotification;
        }
        protected async Task PersistData(IUnitOfWork uow)
        {
            if (!_domainNotification.HasNotifications)
            {
                if (!await uow.Commit()) _domainNotification.AddNotifications(string.Empty,"Error connection database");
            }
        }
    }
}