using MediatR;
using Framework.Core.Messages;
using Activities.Domain.Models.Entities;
using Activities.Domain.Models.Repositories;
using Activities.Domain.ValidatorServices;
using Activities.Application.Commands.DeleteActivity;
using Framework.Core.DomainObjects;
using Framework.Core.Notifications;
using Framework.Core.Mediator;

namespace Activities.Application.Commands.DelteActivity
{
    public class DeleteActivityCommandHandler : CommandHandler<DeleteActivityCommand, Result,DeleteActivityCommandValidation>
    {
        private readonly IActivityRepository _activitytRepository;

        public DeleteActivityCommandHandler(IActivityRepository activitytRepository, IMediatorHandler mediatorHandler, IDomainNotification domainNotification) : base(domainNotification, mediatorHandler)
        {
            _activitytRepository = activitytRepository;
        }

        public override async Task<Result> ExecutCommand(DeleteActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = _activitytRepository.GetById(request.ActivityId) ?? throw new DomainException($"The Activity {request.ActivityId} not exists");

            activity.Inactivate(request.CorrelationId);
            _activitytRepository.Update(activity);
            await PersistData(_activitytRepository.UnitOfWork);

            return new Result();
        }
    }
}
