using MediatR;
using Framework.Core.Messages;
using Activities.Domain.Models.Entities;
using Activities.Domain.Models.Repositories;
using Activities.Domain.DomainEvents;
using Activities.Domain.ValidatorServices;
using Activities.Application.Commands.AddActivity;
using Framework.MessageBus;
using Framework.Shared.IntegrationEvent.Integration;
using FluentValidation.Results;
using Framework.Core.Messages.Integration;
using Framework.Core.DomainObjects;
using Framework.Core.Notifications;
using Activities.Application.DomainServices;
using System.ComponentModel.DataAnnotations;
using Activities.Domain.Rules;
using System.Net;
using Framework.Core.Mediator;

namespace Activities.Application.Commands.AddActivity
{
    public class AddActivityCommandHandler : CommandHandler,
    IRequestHandler<AddActivityCommand, AddActivityCommandOutput>
        
    {
        private readonly IActivityRepository _activitytRepository;
        private readonly IActivityValidatorService _activityValidatorService;

        public AddActivityCommandHandler(IActivityRepository activitytRepository,
                                         IActivityValidatorService activityValidatorService,
                                         IDomainNotification domainNotification,
                                         IMediatorHandler mediatorHandler) : base(domainNotification, mediatorHandler)

        {
            _activitytRepository = activitytRepository;
            _activityValidatorService = activityValidatorService;
        }
        public async Task<AddActivityCommandOutput> Handle(AddActivityCommand request, CancellationToken cancellationToken)
        {

            var activity = Activity.Create(request.Workers,
                                    request.TypeActivityBuild,
                                    request.TimeActivityStart,
                                    request.TimeActivityEnd,
                                    request.CorrelationId);

            _domainNotification.AddNotifications(CheckCreateActivityRules(activity));

            _activitytRepository.Add(activity);

            await PersistDataOrRollBackEvent(_activitytRepository.UnitOfWork, new ActivityNotCreatedEvent(request.CorrelationId));

            if (_domainNotification.HasNotifications)
            {
                // TODO: Rollback in rest integrationEvent
                return new AddActivityCommandOutput();
            }

            return new AddActivityCommandOutput
            {
                ActivityId = activity.AggregateId,
                TimeActivityStart = activity.TimeActivityStart,
                TimeActivityEnd = activity.TimeActivityEnd,
                TypeActivityBuild = activity.TypeActivityBuild,
                Workers = request.Workers
            };
        }

        public List<NotificationMessage> CheckCreateActivityRules(Activity activity)
        {
            var lstWorkers = activity.GetWorkers().Select(x => x.WorkerId).ToList();
            var lstNotifications = new List<NotificationMessage>();

            lstNotifications.AddRange(BusinessRuleValidation.Check(new WorkerDuplicatedRule(lstWorkers)));
            lstNotifications.AddRange(BusinessRuleValidation.Check(new ActivityWithTypeBuildComponentAcceptJustOneWorkerRule(activity.TypeActivityBuild, lstWorkers)));
            lstNotifications.AddRange(BusinessRuleValidation.Check(new WorkerInActivityRule(_activityValidatorService,
                                         lstWorkers,
                                         activity.TimeActivityStart,
                                         activity.TimeActivityEnd, null)));

            return lstNotifications;
        }

    }
}