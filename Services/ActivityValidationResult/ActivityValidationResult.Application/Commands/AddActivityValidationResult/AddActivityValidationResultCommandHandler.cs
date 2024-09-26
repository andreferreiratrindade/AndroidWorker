using Framework.Core.Mediator;
using Framework.Core.Messages;
using Framework.Core.Notifications;
using MediatR;
using ActivityValidationResult.Domain.Models.Repositories;
using ActivityValidationResult.Domain.Models.Entities;

namespace ActivityValidationResult.Application.Commands.AddActivityValidationResult
{
    public class AddActivityValidationResultCommandHandler : CommandHandler<AddActivityValidationResultCommand, AddActivityValidationResultCommandOutput,AddActivityValidationResutCommandValidation>
    {
        private readonly IActivityValidationResultRepository _activityValidationResultRepository;


        public AddActivityValidationResultCommandHandler(IActivityValidationResultRepository ActivityValidationResultRepository,
                                     IDomainNotification domainNotification,
                                     IMediatorHandler _mediatorHandler) : base(domainNotification, _mediatorHandler)
        {
            this._activityValidationResultRepository = ActivityValidationResultRepository;
        }
        public override async Task<AddActivityValidationResultCommandOutput> ExecutCommand(AddActivityValidationResultCommand request, CancellationToken cancellationToken)
        {

            var activity = ActivityValidationResultEntity.Create(
                    request.ActivityId,
                    request.TypeActivityBuild,
                    request.TimeActivityStart,
                    request.TimeActivityEnd,
                    request.Workers, request.CorrelationId);


            await _activityValidationResultRepository.Add(activity);


            await PublishEvents(activity);

            return new AddActivityValidationResultCommandOutput();
        }

    }
}
