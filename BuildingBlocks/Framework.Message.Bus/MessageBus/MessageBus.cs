

using Framework.Core.Messages.Integration;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Framework.MessageBus

{
    public class MessageBus : IMessageBus
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger _logger;

        public MessageBus(IPublishEndpoint publishEndpoint, ILogger<MessageBus> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger= logger;
        }
        public void Dispose()
        {

        }

        public Task PublishAsync<T>(T message, CancellationToken cancellationToken) where T : IntegrationEvent
        {
            var messageType = message.GetType().Name;
            _logger.LogInformation($"{LogConstants.SEND_TO_BROKER}: {message.GetType().Name} : {JsonSerializer.Serialize(message)}");
            return _publishEndpoint.Publish(message,cancellationToken);
        }
    }

}
