using Framework.Core.Messages;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Framework.Core.Mediator
{

    public class RequestResponseLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
            where TRequest : Command<TResponse>

    {
        private readonly ILogger _logger;
        public RequestResponseLoggingBehavior(ILogger<RequestResponseLoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            var requestJson = JsonSerializer.Serialize(request);
            _logger.LogInformation($"{LogConstants.SERVICE}: {request.MessageType} : Request : CorrelationId: {request.CorrelationId}: message: {requestJson}");

            var response = await next();

            return response;
        }
    }
}
