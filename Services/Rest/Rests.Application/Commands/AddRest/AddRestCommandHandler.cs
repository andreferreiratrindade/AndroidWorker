using FluentValidation.Results;
using Framework.Core.DomainObjects;
using Framework.Core.Mediator;
using Framework.Core.Messages;
using Framework.Core.Notifications;
using MassTransit;
using MediatR;
using Rests.Domain.DomainEvents;
using Rests.Domain.Models.Entities;
using Rests.Domain.Models.Repositories;
using Rests.Domain.Rules;
using Rests.Domain.ValidationServices;

namespace Rests.Application.Commands.AddRest
{
    public class AddRestCommandHandler : CommandHandler,
    IRequestHandler<AddRestIntegratedCommand, AddRestCommandOutput>
    {
        private readonly IRestRepository _restRepository;
        private readonly IRestValidatorService _restValidatorService;


        public AddRestCommandHandler(IRestRepository restRepository,
                                     IDomainNotification domainNotification,
                                     IRestValidatorService restValidatorService,
                                     IMediatorHandler _mediatorHandler) : base(domainNotification, _mediatorHandler)
        {
            this._restRepository = restRepository;
            this._restValidatorService = restValidatorService;
        }
        public async Task<AddRestCommandOutput> Handle(AddRestIntegratedCommand request, CancellationToken cancellationToken)
        {

            var rest = Rest.Create(request.ActivityId,
                                       request.WorkerId,
                                       request.TypeActivityBuild,
                                       request.TimeRestStart,
                                       request.CorrelationId);

            _domainNotification.AddNotifications(CheckCreateRules(rest, request.TimeActivityStart));
            _restRepository.Add(rest);


            await PersistDataOrRollBackEvent(_restRepository.UnitOfWork,rest, new RestRejectedEvent(request.ActivityId, request.WorkerId));

            return new AddRestCommandOutput();
        }

        public List<NotificationMessage> CheckCreateRules(Rest rest, DateTime timeActivityStart)
        {
            var notifications = new List<NotificationMessage>();
            notifications.AddRange(BusinessRuleValidation.Check(new WorkerInRestRule(_restValidatorService,
                                                                                                  rest.WorkerId,
                                                                                                  timeActivityStart,
                                                                                                  rest.TimeRestEnd,
                                                                                                  Guid.Empty)));

            return notifications;

        }
    }
}