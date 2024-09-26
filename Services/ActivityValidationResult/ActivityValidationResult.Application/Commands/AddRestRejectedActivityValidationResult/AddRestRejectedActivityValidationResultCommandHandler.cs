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
    public class AddRestRejectedActivityValidationResultCommandHandler : CommandHandler<AddRestRejectedActivityValidationResultCommand, AddRestRejectedActivityValidationResultCommandOutput,AddRestRejectedActivityValidationResultCommandValidation>
    {
        private readonly IActivityValidationResultRepository _activityValidationResultRepository;

        public AddRestRejectedActivityValidationResultCommandHandler(IActivityValidationResultRepository ActivityValidationResultRepository,
                                     IDomainNotification domainNotification,
                                     IMediatorHandler mediatorHandler) : base(domainNotification, mediatorHandler)
        {
            this._activityValidationResultRepository = ActivityValidationResultRepository;
        }
        public override async Task<AddRestRejectedActivityValidationResultCommandOutput> ExecutCommand(AddRestRejectedActivityValidationResultCommand request, CancellationToken cancellationToken)
        {
            var model = await _activityValidationResultRepository.GetByActivityId(request.ActivityId);

            model.AddRestRejected(request.WorkerId, request.DescriptionErrors, request.CorrelationId);

            model.TryFinishValidation(request.CorrelationId);

            if(!_domainNotification.HasNotifications){
                await _activityValidationResultRepository.Update(model);
            }

            await PublishEvents(model);

            return request.GetCommandOutput();
        }

    }
}
