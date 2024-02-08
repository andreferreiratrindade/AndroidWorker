using Framework.Core.Mediator;
using Framework.Shared.IntegrationEvent.Integration;
using Rests.Application.Commands.AddRest;
using Rests.Domain.Enums;
using MassTransit;

namespace Rests.Api.IntegrationService
{
    public class Rest_ActivityCreatedEventHandler : IConsumer<ActivityCreatedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IPublishEndpoint _publishEndpoint;

        public Rest_ActivityCreatedEventHandler(IServiceProvider serviceProvider, IPublishEndpoint publishEndpoint)
        {
            _serviceProvider = serviceProvider;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<ActivityCreatedIntegrationEvent> context)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();

            // context.Message.Workers.ForEach(async workerId =>
            // {

                await mediator.SendCommand<AddRestIntegratedCommand, AddRestCommandOutput>(new AddRestIntegratedCommand(context.Message.ActivityId,
                                    (TypeActivityBuild)context.Message.TypeActivityBuild,
                                    context.Message.TimeActivityStart,
                                    context.Message.TimeActivityEnd,
                                    context.Message.Workers[0],
                                    context.Message.CorrelationId));
            // });

        }
    }


}