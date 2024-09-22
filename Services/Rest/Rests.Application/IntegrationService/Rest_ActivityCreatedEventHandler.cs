using Framework.Core.Mediator;
using Framework.Shared.IntegrationEvent.Integration;
using Rests.Application.Commands.AddRest;
using Rests.Domain.Enums;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Rests.Application.IntegrationService
{
    public class Rest_ActivityValidationResultCreatedHandler : IConsumer<ActivityValidationResultCreatedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public Rest_ActivityValidationResultCreatedHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Consume(ConsumeContext<ActivityValidationResultCreatedIntegrationEvent> context)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();

            // context.Message.Workers.ForEach(async x =>
            // {
                await mediator.SendCommand<AddRestIntegratedCommand, AddRestCommandOutput>(
                    new AddRestIntegratedCommand(context.Message.ActivityId,
                                        (TypeActivityBuild)context.Message.TypeActivityBuild,
                                        context.Message.TimeActivityStart,
                                        context.Message.TimeActivityEnd,
                                        context.Message.Workers[0],
                                        context.Message.CorrelationId));
            // });

        }
    }
}
