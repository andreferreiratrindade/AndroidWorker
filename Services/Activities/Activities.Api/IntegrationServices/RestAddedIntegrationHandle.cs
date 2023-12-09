﻿using Activities.Application.Commands.UpdateTimeStartAndTimeEndActivity;
using Activities.Domain.Enums;
using Framework.Core.Mediator;
using Framework.Shared.IntegrationEvent.Integration;
using MassTransit;

namespace Activities.Api.IntegrationServices
{
    public class RestAddedIntegrationHandle : IConsumer<RestAddedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IPublishEndpoint _publishEndpoint;

        public RestAddedIntegrationHandle(IServiceProvider serviceProvider, IPublishEndpoint publishEndpoint)
        {
            _serviceProvider = serviceProvider;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<RestAddedIntegrationEvent> context)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            _ = await mediator.SendCommand<ConfirmActivityWorkerCommand, ConfirmActivityWorkerCommandOutput>(
                    new ConfirmActivityWorkerCommand(
                        context.Message.ActivityId,
                        context.Message.RestId,
                        context.Message.WorkerId));

        }
    }
}