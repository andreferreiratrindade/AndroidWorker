using Activities.Application.Commands.UpdateTimeStartAndTimeEndActivity;
using Activities.Domain.Enums;
using Framework.Core.Mediator;
using Framework.Shared.IntegrationEvent.Integration;
using MassTransit;

namespace Activities.Api.IntegrationServices
{
    public class Activity_ActivityAcceptedIntegrationHandle : IConsumer<ActivityAcceptedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public Activity_ActivityAcceptedIntegrationHandle(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Consume(ConsumeContext<ActivityAcceptedIntegrationEvent> context)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            _ = await mediator.SendCommand<ConfirmActivityCommand, ConfirmActivityCommandOutput>(
                    new ConfirmActivityCommand(
                        context.Message.ActivityId));

        }
    }
}