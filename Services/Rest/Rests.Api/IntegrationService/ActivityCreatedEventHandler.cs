using Activities.Application.IntegrationEvents;
using Framework.Core.Mediator;
using Framework.Core.Messages;
using Framework.Core.Messages.Integration;
using Framework.MessageBus;
using Framework.Shared.IntegrationEvent.Integration;
using Rests.Application.Commands.AddRest;
using Rests.Application.Commands.UpdateTimeStartAndEndRest;
using Rests.Domain.Enums;
using FluentValidation.Results;
using Framework.Core.Notifications;
using MassTransit;
using Rests.Domain.DomainEvents;

namespace Rests.Api.IntegrationService
{
    public class ActivityCreatedEventHandler : IConsumer<ActivityCreatedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IPublishEndpoint _publishEndpoint;

        public ActivityCreatedEventHandler( IServiceProvider serviceProvider, IPublishEndpoint publishEndpoint)
        {
            _serviceProvider = serviceProvider;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<ActivityCreatedIntegrationEvent> context)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            await mediator.SendCommand<AddRestIntegratedCommand, AddRestCommandOutput>(new AddRestIntegratedCommand(context.Message.ActivityId,
                                        (TypeActivityBuild) context.Message.TypeActivityBuild,
                                        context.Message.TimeActivityStart,
                                        context.Message.TimeActivityEnd,
                                        context.Message.Workers,
                                        context.Message.CorrelationId));

        }
    }


}