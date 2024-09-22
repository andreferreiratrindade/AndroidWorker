using MediatR;
using Framework.Core.Messages;
using Framework.Core.DomainObjects;
using Framework.Core.Notifications;
using Framework.Core.Mediator;
using Worker.Domain.Models.Entities;
using Worker.Domain.Models.Repositories;

namespace Worker.Application.Commands.AddWorker
{
    public class AddWorkerCommandHandler : CommandHandler,
    IRequestHandler<AddWorkerCommand, AddWorkerCommandOutput>

    {
        private readonly IWorkerRepository _workerRepository;

        public AddWorkerCommandHandler(IWorkerRepository workerRepository,
            IDomainNotification domainNotification,
                                         IMediatorHandler mediatorHandler) : base(domainNotification, mediatorHandler)

        {
            this._workerRepository = workerRepository;
        }


        public async Task<AddWorkerCommandOutput> Handle(AddWorkerCommand request, CancellationToken cancellationToken)
        {
            var workerFromDataBase = _workerRepository.GetQueryable().Select(x=> x.WorkerId).ToList();
            var workerToCreated = request.Workers.Where(x=> !workerFromDataBase.Any(y=> y == x)).ToList();
            List<WorkerEntity> workersCreated = [];
            workerToCreated.ForEach(x=>{
                var workerEntity = WorkerEntity.Create(x, request.CorrelationId);
                _workerRepository.Add(workerEntity);
                workersCreated.Add(workerEntity);
            });

            workersCreated.ForEach(async x=> {
                await PublishEventsOrRollBackEvent(x, null);
            });

           return new AddWorkerCommandOutput{ Workers = workersCreated.Select(x=> x.WorkerId).ToList()};
        }
    }
}
