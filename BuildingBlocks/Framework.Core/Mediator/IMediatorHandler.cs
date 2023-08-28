using System.Threading.Tasks;
using FluentValidation.Results;
using Framework.Core.Messages;
using Microsoft.EntityFrameworkCore;

namespace Framework.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T e) where T : Event;
        Task PublishEventDbContext(DbContext dbcontext);
        Task<R> SendCommand<T,R>(T comando)
            where T : Command<R>
            where R : class;
        Task<object> Send(object request, CancellationToken cancellationToken = default);
    }
}