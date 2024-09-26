using Framework.Core.DomainObjects;
using Framework.Core.Mediator;
using Framework.Core.Messages;
using Framework.Core.Notifications;
using MediatR;
using ActivityValidationResult.Domain.DomainEvents;
using ActivityValidationResult.Domain.Models.Repositories;
using ActivityValidationResult.Domain.Models.Entities;
using ActivityValidationResult.Application.Commands.AddRestAcceptedActivityValidationResult;

namespace ActivityValidationResult.Application.Commands.AddActivityValidationResult
{
    public class AddRestAcceptedActivityValidationResultCommandHandler : CommandHandler<AddRestAcceptedActivityValidationResultCommand, AddRestAcceptedActivityValidationResultCommandOutput,AddRestAcceptedActivityValidationResultCommandValidation>
    {
        private readonly IActivityValidationResultRepository _activityValidationResultRepository;

        public AddRestAcceptedActivityValidationResultCommandHandler(IActivityValidationResultRepository ActivityValidationResultRepository,
                                     IDomainNotification domainNotification,
                                     IMediatorHandler mediatorHandler) : base(domainNotification, mediatorHandler)
        {
            this._activityValidationResultRepository = ActivityValidationResultRepository;
        }
        public override async Task<AddRestAcceptedActivityValidationResultCommandOutput> ExecutCommand(AddRestAcceptedActivityValidationResultCommand request, CancellationToken cancellationToken)
        {
            var model = await _activityValidationResultRepository.GetByActivityId(request.ActivityId);

            model.AddRestAccepted(request.RestId, request.TimeRestStart, request.TimeRestEnd, request.WorkerId, request.CorrelationId);

            model.TryFinishValidation(request.CorrelationId);

            if(!_domainNotification.HasNotifications){
                await _activityValidationResultRepository.Update(model);
            }

            await PublishEvents(model);

            return request.GetCommandOutput();
        }

    }
}
