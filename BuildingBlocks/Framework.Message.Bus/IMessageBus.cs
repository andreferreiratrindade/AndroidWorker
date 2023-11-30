using System;
using System.Threading.Tasks;
using EasyNetQ;
using Framework.Core.Messages.Integration;

namespace Framework.MessageBus
{
    public interface IMessageBus : IDisposable
    {


        Task PublishAsync<T>(T message) where T : IntegrationEvent;


        Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage;

    }
}