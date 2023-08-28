using MediatR;
using Framework.Core.Messages;
using Activities.Domain.Models.Entities;
using Activities.Domain.Models.Repositories;
using Activities.Domain.ValidatorServices;
using Activities.Application.Commands.DeleteActivity;
using Framework.Core.DomainObjects;
using Framework.Core.Notifications;

namespace Activities.Application.Commands.DelteActivity
{
    public class DeleteActivityCommandHandler : CommandHandler,
    IRequestHandler<DeleteActivityCommand, Result>
    {
        private readonly IActivityRepository _activitytRepository;

        public DeleteActivityCommandHandler(IActivityRepository activitytRepository, IDomainNotification domainNotification): base(domainNotification) 
        {
            _activitytRepository = activitytRepository;
        }

        public async Task<Result> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = _activitytRepository.GetById(request.ActivityId) ?? throw new DomainException($"The Activity {request.ActivityId} not exists");
        
            activity.Inactivate();
            _activitytRepository.Update(activity);
            await PersistData(_activitytRepository.UnitOfWork);

            return new Result();
        }
    }
}