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

namespace Activities.Application.Commands.RejectActivity
{
    public class RejectActivityCommandHandler : CommandHandler<RejectActivityCommand, RejectActivityCommandOutput,RejectActivityCommandValidation>
    {
        private readonly IActivityRepository _activitytRepository;
        private readonly IActivityValidatorService _activityValidatorService;

        public RejectActivityCommandHandler(IActivityRepository activitytRepository, IActivityValidatorService activityValidatorService, IMediatorHandler mediatorHandler, IDomainNotification domainNotification) : base(domainNotification, mediatorHandler)
        {
            _activitytRepository = activitytRepository;
            _activityValidatorService = activityValidatorService;
        }
        public override async Task<RejectActivityCommandOutput> ExecutCommand(RejectActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = _activitytRepository.GetById(request.ActivityId);

            if(activity == null)
            {
                _domainNotification.AddNotification("Id Not Exists", $"The Activity {request.ActivityId} not exists");
                return new RejectActivityCommandOutput();
            }

            activity.RejectActivity(request.CorrelationId);
            //_domainNotification.AddNotification(CheckConfirmStatus(activity));


            _activitytRepository.Update(activity);

            await PersistData(_activitytRepository.UnitOfWork);


            if (_domainNotification.HasNotifications) return new RejectActivityCommandOutput();

            return new RejectActivityCommandOutput
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
