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

        public async Task PublishEvent<T>(T e) where T : Event
        {
            await _mediator.Publish(e);
        }

        public async Task PublishEventDbContext(DbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notificacoes?.Any() == true);

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Notificacoes)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearEvent());

            var tasks = domainEvents
                .Select(async (domainEvent) => await PublishEvent(domainEvent));
            await Task.WhenAll(tasks);
        }



        public async Task<object> Send(object request, CancellationToken cancellationToken = default) => await _mediator.Send(request);
    }
}