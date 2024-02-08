using MediatR;
using Framework.Core.Messages;
using Activities.Domain.Models.Entities;
using Activities.Domain.Models.Repositories;
using Activities.Domain.DomainEvents;
using Activities.Domain.ValidatorServices;
using Framework.Core.DomainObjects;
using Framework.Core.Notifications;
using Activities.Domain.Rules;
using Framework.Core.Mediator;

namespace Activities.Application.Commands.UpdateTimeStartAndTimeEndActivity
{
    public class ConfirmActivityCommandHandler : CommandHandler,
    IRequestHandler<ConfirmActivityCommand, ConfirmActivityCommandOutput>
    {
        private readonly IActivityRepository _activitytRepository;
        private readonly IActivityValidatorService _activityValidatorService;

        public ConfirmActivityCommandHandler(IActivityRepository activitytRepository, IActivityValidatorService activityValidatorService, IMediatorHandler mediatorHandler, IDomainNotification domainNotification) : base(domainNotification, mediatorHandler)
        {
            _activitytRepository = activitytRepository;
            _activityValidatorService = activityValidatorService;
        }
        public async Task<ConfirmActivityCommandOutput> Handle(ConfirmActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = _activitytRepository.GetById(request.ActivityId);

            if(activity == null)
            {
                _domainNotification.AddNotifications("Id Not Exists", $"The Activity {request.ActivityId} not exists");
                return new ConfirmActivityCommandOutput();
            }

            activity.ConfirmActivity();

            _activitytRepository.Update(activity);

            await PersistDataOrRollBackEvent(_activitytRepository.UnitOfWork,activity, new ActivityNotCreatedEvent(request.ActivityId));

            if (_domainNotification.HasNotifications) return new ConfirmActivityCommandOutput();

            return new ConfirmActivityCommandOutput
            {
                ActivityId = activity.AggregateId,
                TimeActivityStart = activity.TimeActivityStart,
                TimeActivityEnd = activity.TimeActivityEnd,
                TypeActivityBuild = activity.TypeActivityBuild,
            };
        }


        public List<NotificationMessage> CheckConfirmStatus(Activity activity)
        {

            return BusinessRuleValidation.Check(new WorkerInActivityRule(_activityValidatorService,
                                          activity.GetWorkers().Select(x => x.WorkerId).ToList(),
                                          activity.TimeActivityStart,
                                          activity.TimeActivityEnd, activity.AggregateId));
        }

    }
}