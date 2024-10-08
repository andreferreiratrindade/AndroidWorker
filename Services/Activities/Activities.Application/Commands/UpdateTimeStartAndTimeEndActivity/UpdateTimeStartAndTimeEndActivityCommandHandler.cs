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
    public class UpdateTimeStartAndTimeEndActivityCommandHandler : CommandHandler<UpdateTimeStartAndTimeEndActivityCommand, UpdateTimeStartAndTimeEndActivityCommandOutput, UpdateTimeStartAndTimeEndActivityCommandValidation>
    {
        private readonly IActivityRepository _activitytRepository;
        private readonly IActivityValidatorService _activityValidatorService;

        public UpdateTimeStartAndTimeEndActivityCommandHandler(IActivityRepository activitytRepository, IActivityValidatorService activityValidatorService, IMediatorHandler mediatorHandler, IDomainNotification domainNotification) : base(domainNotification, mediatorHandler)
        {
            _activitytRepository = activitytRepository;
            _activityValidatorService = activityValidatorService;
        }
        public override async Task<UpdateTimeStartAndTimeEndActivityCommandOutput> ExecutCommand(UpdateTimeStartAndTimeEndActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = _activitytRepository.GetById(request.ActivityId);

            if(activity == null)
            {
                _domainNotification.AddNotification("Id Not Exists", $"The Activity {request.ActivityId} not exists");
                return new UpdateTimeStartAndTimeEndActivityCommandOutput();
            }

            activity.UpdateTimeStartAndTimeEnd(request.TimeActivityStart, request.TimeActivityEnd,_activityValidatorService, request.CorrelationId);

            _domainNotification.AddNotification(CheckUpdateTimeStartAndTimeEnd(activity));

            _activitytRepository.Update(activity);

            await PersistData(_activitytRepository.UnitOfWork);

            if (_domainNotification.HasNotifications) return request.GetCommandOutput();

            return new UpdateTimeStartAndTimeEndActivityCommandOutput
            {
                ActivityId = activity.AggregateId,
                TimeActivityStart = activity.TimeActivityStart,
                TimeActivityEnd = activity.TimeActivityEnd,
                TypeActivityBuild = activity.TypeActivityBuild,
            };
        }


        public List<NotificationMessage> CheckUpdateTimeStartAndTimeEnd(Activity activity)
        {

            return BusinessRuleValidation.Check(new WorkerInActivityRule(_activityValidatorService,
                                          activity.GetWorkers().Select(x => x.WorkerId).ToList(),
                                          activity.TimeActivityStart,
                                          activity.TimeActivityEnd, activity.AggregateId));
        }

    }
}
