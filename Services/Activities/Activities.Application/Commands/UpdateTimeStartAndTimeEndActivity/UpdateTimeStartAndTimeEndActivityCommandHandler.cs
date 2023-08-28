using MediatR;
using Framework.Core.Messages;
using Activities.Domain.Models.Entities;
using Activities.Domain.Models.Repositories;
using Activities.Domain.DomainEvents;
using Activities.Domain.ValidatorServices;
using Framework.Core.DomainObjects;
using Framework.Core.Notifications;
using Activities.Domain.Rules;

namespace Activities.Application.Commands.UpdateTimeStartAndTimeEndActivity
{
    public class UpdateTimeStartAndTimeEndActivityCommandHandler : CommandHandler,
    IRequestHandler<UpdateTimeStartAndTimeEndActivityCommand, UpdateTimeStartAndTimeEndActivityCommandOutput>
    {
        private readonly IActivityRepository _activitytRepository;
        private readonly IActivityValidatorService _activityValidatorService;

        public UpdateTimeStartAndTimeEndActivityCommandHandler(IActivityRepository activitytRepository, IActivityValidatorService activityValidatorService, IDomainNotification domainNotification):base(domainNotification)
        {
            _activitytRepository = activitytRepository;
            _activityValidatorService = activityValidatorService;
        }
        public async Task<UpdateTimeStartAndTimeEndActivityCommandOutput> Handle(UpdateTimeStartAndTimeEndActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = _activitytRepository.GetById(request.ActivityId);

            if(activity == null)
            {
                _domainNotification.AddNotifications("Id Not Exists", $"The Activity {request.ActivityId} not exists");
                return new UpdateTimeStartAndTimeEndActivityCommandOutput();
            }

            activity.UpdateTimeStartAndTimeEnd(request.TimeActivityStart, request.TimeActivityEnd,_activityValidatorService, _domainNotification);
            
            _domainNotification.AddNotifications(CheckUpdateTimeStartAndTimeEnd(activity));


            _activitytRepository.Update(activity);

            await PersistData(_activitytRepository.UnitOfWork);

            if (_domainNotification.HasNotifications) return new UpdateTimeStartAndTimeEndActivityCommandOutput();

            return new UpdateTimeStartAndTimeEndActivityCommandOutput
            {
                ActivityId = activity.Id,
                TimeActivityStart = activity.TimeActivityStart,
                TimeActivityEnd = activity.TimeActivityEnd,
                TypeActivityBuild = activity.TypeActivityBuild,
                TimeRestEnd = activity.TimeRestEnd
            };
        }


        public List<NotificationMessage> CheckUpdateTimeStartAndTimeEnd(Activity activity)
        {

            return BusinessRuleValidation.Check(new WorkerInActivityRule(_activityValidatorService,
                                          activity.GetWorkers().Select(x => x.WorkerId).ToList(),
                                          activity.TimeActivityStart,
                                          activity.TimeActivityEnd, activity.Id));
        }

    }
}