using Activities.Application.Commands.UpdateTimeStartAndTimeEndActivity;
using Framework.Core.Mediator;
using Framework.Shared.IntegrationEvent.Integration;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Activities.Application.IntegrationServices
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
                        context.Message.ActivityId, context.Message.CorrelationId));

        }
    }
}
