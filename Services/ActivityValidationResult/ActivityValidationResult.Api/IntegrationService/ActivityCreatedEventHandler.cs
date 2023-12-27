using Framework.Core.Mediator;
using Framework.Shared.IntegrationEvent.Integration;
using ActivityValidationResult.Application.Commands.AddActivityValidationResult;
using MassTransit;

namespace ActivityValidationResult.Api.IntegrationService
{
    public class ActivityCreatedEventHandler : IConsumer<ActivityCreatedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IPublishEndpoint _publishEndpoint;

        public ActivityCreatedEventHandler(IServiceProvider serviceProvider, IPublishEndpoint publishEndpoint)
        {
            _serviceProvider = serviceProvider;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<ActivityCreatedIntegrationEvent> context)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            await mediator.SendCommand<AddActivityValidationResultCommand, AddActivityValidationResultCommandOutput>(
                new AddActivityValidationResultCommand(context.Message.ActivityId,
                                        context.Message.Workers,
                                        context.Message.CorrelationId));

        }
    }


}