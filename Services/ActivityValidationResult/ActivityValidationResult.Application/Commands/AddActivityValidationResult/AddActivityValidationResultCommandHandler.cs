using Framework.Core.DomainObjects;
using Framework.Core.Mediator;
using Framework.Core.Messages;
using Framework.Core.Notifications;
using MediatR;
using ActivityValidationResult.Domain.DomainEvents;
using ActivityValidationResult.Domain.Models.Repositories;
using ActivityValidationResult.Domain.Models.Entities;

namespace ActivityValidationResult.Application.Commands.AddActivityValidationResult
{
    public class AddActivityValidationResultCommandHandler : CommandHandler,
    IRequestHandler<AddActivityValidationResultCommand, AddActivityValidationResultCommandOutput>
    {
        private readonly IActivityValidationResultRepository _activityValidationResultRepository;


        public AddActivityValidationResultCommandHandler(IActivityValidationResultRepository ActivityValidationResultRepository,
                                     IDomainNotification domainNotification,
                                     IMediatorHandler _mediatorHandler) : base(domainNotification, _mediatorHandler)
        {
            this._activityValidationResultRepository = ActivityValidationResultRepository;
        }
        public async Task<AddActivityValidationResultCommandOutput> Handle(AddActivityValidationResultCommand request, CancellationToken cancellationToken)
        {

            var activity = ActivityValidationResultEntity.Create(request.ActivityId, request.Workers,
                                   request.CorrelationId);


            await _activityValidationResultRepository.Add(activity);


           // await PersistDataOrRollBackEvent(_activityValidationResultRepository.UnitOfWork, new ActivityRejectedEvent(request.ActivityId,request.CorrelationId));

            return new AddActivityValidationResultCommandOutput();
        }

    }
}