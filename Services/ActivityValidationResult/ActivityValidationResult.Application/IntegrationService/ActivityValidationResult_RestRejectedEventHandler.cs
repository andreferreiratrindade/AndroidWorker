using Framework.Core.Mediator;
using Framework.Shared.IntegrationEvent.Integration;
using ActivityValidationResult.Application.Commands.AddActivityValidationResult;
using MassTransit;
using ActivityValidationResult.Application.Commands.AddRestAcceptedActivityValidationResult;
using ActivityValidationResult.Application.Commands.AddRestRejectedActivityValidationResult;
using Microsoft.Extensions.DependencyInjection;

namespace ActivityValidationResult.Application.IntegrationService
{
    public class ActivityValidationResult_RestRejectedEventHandler :
            IConsumer<RestRejectedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public ActivityValidationResult_RestRejectedEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

            public async Task Consume(ConsumeContext<RestRejectedIntegrationEvent> context)
        {
             using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            await mediator.SendCommand<AddRestRejectedActivityValidationResultCommand, AddRestRejectedActivityValidationResultCommandOutput>(
                new AddRestRejectedActivityValidationResultCommand(context.Message.ActivityId,
                                        context.Message.WorkerId,
                                        context.Message.DescriptionErros,
                                        context.Message.CorrelationId));
        }
    }
}
