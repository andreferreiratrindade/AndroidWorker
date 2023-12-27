using Framework.Core.Mediator;
using Framework.Shared.IntegrationEvent.Integration;
using ActivityValidationResult.Application.Commands.AddActivityValidationResult;
using MassTransit;
using ActivityValidationResult.Application.Commands.AddRestAcceptedActivityValidationResult;

namespace ActivityValidationResult.Api.IntegrationService
{
    public class RestAddedEventHandler : IConsumer<RestAddedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public RestAddedEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Consume(ConsumeContext<RestAddedIntegrationEvent> context)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            await mediator.SendCommand<AddRestAcceptedActivityValidationResultCommand, AddRestAcceptedActivityValidationResultCommandOutput>(
                new AddRestAcceptedActivityValidationResultCommand(context.Message.ActivityId,
                                        context.Message.RestId,
                                        context.Message.WorkerId, 
                                        context.Message.TimeRestStart,
                                        context.Message.TimeRestEnd,
                                        context.Message.CorrelationId));
        }
    }
}