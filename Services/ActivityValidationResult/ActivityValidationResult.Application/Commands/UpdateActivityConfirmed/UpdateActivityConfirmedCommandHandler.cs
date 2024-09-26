using Framework.Core.DomainObjects;
using Framework.Core.Mediator;
using Framework.Core.Messages;
using Framework.Core.Notifications;
using MediatR;
using ActivityValidationResult.Domain.DomainEvents;
using ActivityValidationResult.Domain.Models.Repositories;
using ActivityValidationResult.Domain.Models.Entities;

namespace ActivityValidationResult.Application.Commands.UpdateActivityConfirmed
{
    public class UpdateActivityConfirmedCommandHandler : CommandHandler<UpdateActivityConfirmedCommand, UpdateActivityConfirmedCommandOutput,UpdateActivityConfirmedCommandValidation>
    {
        private readonly IActivityValidationResultRepository _activityValidationResultRepository;


        public UpdateActivityConfirmedCommandHandler(IActivityValidationResultRepository ActivityValidationResultRepository,
                                     IDomainNotification domainNotification,
                                     IMediatorHandler _mediatorHandler) : base(domainNotification, _mediatorHandler)
        {
            this._activityValidationResultRepository = ActivityValidationResultRepository;
        }
        public override async Task<UpdateActivityConfirmedCommandOutput> ExecutCommand(UpdateActivityConfirmedCommand request, CancellationToken cancellationToken)
        {
            var activityValidator = await _activityValidationResultRepository.GetByActivityId(request.ActivityId);

           activityValidator.UpdateActivityConfirmed(request.CorrelationId);


            // await _activityValidationResultRepository.Add(activity);


            // await PublishEvents(activity, null);

            return request.GetCommandOutput();
        }

    }
}
