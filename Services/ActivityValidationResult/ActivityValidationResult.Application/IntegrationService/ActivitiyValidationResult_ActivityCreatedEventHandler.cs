using Framework.Core.Mediator;
using Framework.Shared.IntegrationEvent.Integration;
using ActivityValidationResult.Application.Commands.AddActivityValidationResult;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Framework.Shared.IntegrationEvent.Enums;

namespace ActivityValidationResult.Application.IntegrationService
{
    public class ActivitiyValidationResult_ActivityCreatedEventHandler : IConsumer<ActivityCreatedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;


        public ActivitiyValidationResult_ActivityCreatedEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Consume(ConsumeContext<ActivityCreatedIntegrationEvent> context)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            await mediator.SendCommand<AddActivityValidationResultCommand, AddActivityValidationResultCommandOutput>(
                new AddActivityValidationResultCommand(
                                    context.Message.ActivityId,
                                    (TypeActivityBuild)context.Message.TypeActivityBuild,
                                    context.Message.TimeActivityStart,
                                    context.Message.TimeActivityEnd,
                                    context.Message.Workers,
                                    context.Message.CorrelationId));

        }
    }


}
