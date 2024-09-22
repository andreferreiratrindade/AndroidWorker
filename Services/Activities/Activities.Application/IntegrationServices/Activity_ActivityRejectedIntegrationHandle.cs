using Activities.Application.Commands.RejectActivity;
using Framework.Core.Mediator;
using Framework.MessageBus;
using Framework.Shared.IntegrationEvent.Integration;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Activities.Application.IntegrationServices
{
    public class Activity_ActivityRejectedIntegrationHandle : IConsumer<ActivityRejectedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;


        public Activity_ActivityRejectedIntegrationHandle(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Consume(ConsumeContext<ActivityRejectedIntegrationEvent> context)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            _ = await mediator.SendCommand<RejectActivityCommand, RejectActivityCommandOutput>(
                    new RejectActivityCommand(
                        context.Message.ActivityId, context.Message.CorrelationId));

        }
    }
}
