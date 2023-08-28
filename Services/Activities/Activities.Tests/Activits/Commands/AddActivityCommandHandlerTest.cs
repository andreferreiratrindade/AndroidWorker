using MediatR;
using Framework.Core.Notifications;
using Framework.MessageBus;
using Framework.Shared.IntegrationEvent.Integration;
using Activities.Domain.Models.Repositories;
using Activities.Domain.Rules;
using Activities.Application.DomainServices;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Activities.Application.Commands.AddActivity
{
    public class AddActivityCommandHandler : IRequestHandler<AddActivityCommand, AddActivityCommandOutput>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMessageBus _messageBus;
        private readonly IDomainNotification _domainNotification;
        private readonly IActivityValidationService _activityValidationService;

        public AddActivityCommandHandler(
            IActivityRepository activityRepository,
            IMessageBus messageBus,
            IDomainNotification domainNotification,
            IActivityValidationService activityValidationService)
        {
            _activityRepository = activityRepository;
            _messageBus = messageBus;
            _domainNotification = domainNotification;
            _activityValidationService = activityValidationService;
        }

        public async Task<AddActivityCommandOutput> Handle(AddActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = CreateActivity(request);

            _domainNotification.AddNotifications(activity.CheckRules());

            if (_domainNotification.HasNotifications)
            {
                return new AddActivityCommandOutput();
            }

            _activityRepository.Add(activity);

            var addRestResult = await _messageBus.RequestAsync<AddRestIntegrationEvent, ResponseMessage>(new AddRestIntegrationEvent(activity.Id, request.Workers, activity.TypeActivityBuild.GetHashCode(), activity.TimeActivityStart, activity.TimeActivityEnd));

            _domainNotification.AddNotifications(addRestResult.Notifications);

            await PersistData(_activityRepository.UnitOfWork);

            if (_domainNotification.HasNotifications)
            {
                // TODO: Rollback in rest integrationEvent
                return new AddActivityCommandOutput();
            }

            return new AddActivityCommandOutput
            {
                ActivityId = activity.Id,
                TimeActivityStart = activity.TimeActivityStart,
                TimeActivityEnd = activity.TimeActivityEnd,
                TypeActivityBuild = activity.TypeActivityBuild,
                Workers = request.Workers
            };
        }

        private Activity CreateActivity(AddActivityCommand request)
        {
            return Activity.Create(
                request.Workers,
                request.TypeActivityBuild,
                request.TimeActivityStart,
                request.TimeActivityEnd);
        }

        private async Task PersistData(IUnitOfWork unitOfWork)
        {
            await unitOfWork.Commit();
        }
    }
}
