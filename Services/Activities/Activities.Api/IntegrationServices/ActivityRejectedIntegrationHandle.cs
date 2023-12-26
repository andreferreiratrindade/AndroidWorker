using Activities.Application.Commands.RejectActivity;
using Activities.Application.Commands.UpdateTimeStartAndTimeEndActivity;
using Activities.Domain.Enums;
using Framework.Core.Mediator;
using Framework.Shared.IntegrationEvent.Integration;
using MassTransit;

namespace Activities.Api.IntegrationServices
{
    public class ActivityRejectedIntegrationHandle : IConsumer<ActivityRejectedIntegratedEvent>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IPublishEndpoint _publishEndpoint;

        public ActivityRejectedIntegrationHandle(IServiceProvider serviceProvider, IPublishEndpoint publishEndpoint)
        {
            _serviceProvider = serviceProvider;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<ActivityRejectedIntegratedEvent> context)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            _ = await mediator.SendCommand<RejectActivityCommand, RejectActivityCommandOutput>(
                    new RejectActivityCommand(
                        context.Message.ActivityId));

        }
    }
}