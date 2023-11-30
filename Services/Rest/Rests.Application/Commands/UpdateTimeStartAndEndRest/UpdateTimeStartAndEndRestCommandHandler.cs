using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Core.Mediator;
using Framework.Core.Messages;
using Framework.Core.Notifications;
using MediatR;
using Rests.Domain.Models.Repositories;


namespace Rests.Application.Commands.UpdateTimeStartAndEndRest
{
    public class UpdateTimeStartAndEndRestCommandHandler : CommandHandler,
    IRequestHandler<UpdateTimeStartAndEndRestCommand, Result>
    {
        private readonly IRestRepository _restRepository;

        public UpdateTimeStartAndEndRestCommandHandler(IRestRepository restRepository, IMediatorHandler mediatorHandler, IDomainNotification domainNotification): base(domainNotification, mediatorHandler)
        {
            this._restRepository = restRepository;
        }
        public async Task<Result> Handle(UpdateTimeStartAndEndRestCommand request, CancellationToken cancellationToken)
        {

            var lstRest = _restRepository.GetByActivityId(request.ActivityId);

            foreach (var item in lstRest)
            {
                item.UpdateTimeRestEnd(request.TimeActivityEnd);
            }
            await PersistData(_restRepository.UnitOfWork);

            return new Result();
        }
    }
}