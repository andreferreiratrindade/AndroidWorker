

using Framework.Core.Mediator;
using Framework.Shared.IntegrationEvent.Integration;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Worker.Application.Commands.AddWorker;

namespace Worker.Application.IntegrationServices
{
    public class Worker_ActivityConfirmedIntegrationHandle(IServiceProvider serviceProvider) : IConsumer<ActivityConfirmedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public async Task Consume(ConsumeContext<ActivityConfirmedIntegrationEvent> context)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            _ = await mediator.SendCommand<AddWorkerCommand, AddWorkerCommandOutput>(
                    new AddWorkerCommand(
                        context.Message.Workers, context.Message.CorrelationId));

        }
    }
}
