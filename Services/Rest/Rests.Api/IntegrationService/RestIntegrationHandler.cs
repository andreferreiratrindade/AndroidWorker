using Activities.Application.IntegrationEvents;
using Framework.Core.Mediator;
using Framework.Core.Messages;
using Framework.Core.Messages.Integration;
using Framework.MessageBus;
using Framework.Shared.IntegrationEvent.Integration;
using Rests.Application.Commands.AddRest;
using Rests.Application.Commands.UpdateTimeStartAndEndRest;
using Rests.Domain.Enums;
using FluentValidation.Results;
using Framework.Core.Notifications;

namespace Rests.Api.IntegrationService
{
    public class ActivityUptatedTimeStartAndTimeEndEventIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public ActivityUptatedTimeStartAndTimeEndEventIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;

        }
        private void SetSubscribers()
        {
            _bus.RespondAsync<AddRestIntegrationEvent, ResponseMessage>(async request => await AddRestCommandHandler(request));
        }

        private async Task<ResponseMessage> AddRestCommandHandler(AddRestIntegrationEvent request)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            var result = await mediator.SendCommand<AddRestCommand, AddRestCommandOutput>(new AddRestCommand(request.ActivityId,
                                                                  (TypeActivityBuild)request.TypeActivityBuild,
                                                                  request.TimeActivityStart,
                                                                  request.TimeRestStart,
                                                                  request.Workers));
            var domainNotification = scope.ServiceProvider.GetRequiredService<IDomainNotification>();

            return new ResponseMessage(domainNotification.Notifications);
        }

        public async Task ActivityCreatedIntegrationHandler(ActivityCreatedIntegrationEvent request)
        {

        }


        public async Task ActivityUptatedTimeStartAndTimeEndIntegrationHandler(ActivityUptatedTimeStartAndTimeEndIntegrationEvent request)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            await mediator.SendCommand<UpdateTimeStartAndEndRestCommand, Result>(new UpdateTimeStartAndEndRestCommand(request.ActivityId,
                                                                  request.TimeActivityStart,
                                                                  request.TimeActivityEnd));
            await Task.CompletedTask;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetSubscribers();
            return Task.CompletedTask;
        }
    }


}