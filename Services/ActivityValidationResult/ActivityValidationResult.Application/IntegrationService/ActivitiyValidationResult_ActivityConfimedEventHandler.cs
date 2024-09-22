using Framework.Core.Mediator;
using Framework.Shared.IntegrationEvent.Integration;
using ActivityValidationResult.Application.Commands.AddActivityValidationResult;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace ActivityValidationResult.Application.IntegrationService
{
    public class ActivitiyValidationResult_ActivityConfirmedEventHandler : IConsumer<ActivityConfirmedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IPublishEndpoint _publishEndpoint;

        public ActivitiyValidationResult_ActivityConfirmedEventHandler(IServiceProvider serviceProvider, IPublishEndpoint publishEndpoint)
        {
            _serviceProvider = serviceProvider;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<ActivityConfirmedIntegrationEvent> context)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            await mediator.SendCommand<UpdateActivityConfirmedCommand, UpdateActivityConfirmedCommandOutput>(
                new UpdateActivityConfirmedCommand(context.Message.ActivityId,
                                        context.Message.CorrelationId));

        }
    }


}
