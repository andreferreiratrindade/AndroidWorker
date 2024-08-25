using Framework.Core.DomainObjects;
using Framework.Core.Messages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Framework.Core.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<R> SendCommand<T, R>(T comando)
                   where T : Command<R>
                   where R : class
        {
            return await _mediator.Send(comando);
        }

        public async Task PublishEvent(IDomainEvent @event)
        {
            await _mediator.Publish(@event);
        }

        public async Task PublishEvent(RollBackEvent @event)
        {
            await _mediator.Publish(@event);
        }

        public async Task PublishEvent(IEnumerable<IDomainEvent> events)
        {

            var tasks = events
                .Select(async (domainEvent) => await PublishEvent(domainEvent));
            await Task.WhenAll(tasks);
        }



        public async Task<object> Send(object request, CancellationToken cancellationToken = default)
            => await _mediator.Send(request);
    }
}
