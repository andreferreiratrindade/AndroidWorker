using Framework.Core.DomainObjects;
using Framework.Core.Mediator;
using Framework.Core.Messages;
using Framework.Core.Notifications;
using MediatR;
using ActivityValidationResult.Domain.DomainEvents;
using ActivityValidationResult.Domain.Models.Repositories;
using ActivityValidationResult.Domain.Models.Entities;
using ActivityValidationResult.Application.Commands.AddRestRejectedActivityValidationResult;

namespace ActivityValidationResult.Application.Commands.AddRestRejectedActivityValidationResult
{
    public class AddRestRejectedActivityValidationResultCommandHandler : CommandHandler,
    IRequestHandler<AddRestRejectedActivityValidationResultCommand, AddRestRejectedActivityValidationResultCommandOutput>
    {
        private readonly IActivityValidationResultRepository _activityValidationResultRepository;

        public AddRestRejectedActivityValidationResultCommandHandler(IActivityValidationResultRepository ActivityValidationResultRepository,
                                     IDomainNotification domainNotification,
                                     IMediatorHandler _mediatorHandler) : base(domainNotification, _mediatorHandler)
        {
            this._activityValidationResultRepository = ActivityValidationResultRepository;
        }
        public async Task<AddRestRejectedActivityValidationResultCommandOutput> Handle(AddRestRejectedActivityValidationResultCommand request, CancellationToken cancellationToken)
        {
            var model = await _activityValidationResultRepository.GetByActivityId(request.ActivityId);

            model.AddRestRejected(request.WorkerId, request.DescriptionErrors);

            model.TryFinishValidation();     

            if(!_domainNotification.HasNotifications){
                await _activityValidationResultRepository.Update(model);
            }

            await PublishEventsOrRollBackEvent(model, null);

            return new AddRestRejectedActivityValidationResultCommandOutput();
        }

    }
}