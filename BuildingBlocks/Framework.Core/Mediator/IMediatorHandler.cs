using System.ComponentModel;
using System.Threading.Tasks;
using FluentValidation.Results;
using Framework.Core.DomainObjects;
using Framework.Core.Messages;
using Microsoft.EntityFrameworkCore;

namespace Framework.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent(IDomainEvent @event);
        Task PublishEvent(List<IDomainEvent> events);
        Task<R> SendCommand<T,R>(T comando)
            where T : Command<R>
            where R : class;
        Task<object> Send(object request, CancellationToken cancellationToken = default);
    }
}