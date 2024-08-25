using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Framework.Core.Mediator
{

public class RequestResponseLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private ILogger _logger;
    public RequestResponseLoggingBehavior(ILogger<RequestResponseLoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger =logger;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var correlationIdLog = Guid.NewGuid();

        var requestJson = JsonSerializer.Serialize(request);

        _logger.LogInformation("Request for {CorrelationID}: {Request}", correlationIdLog, requestJson);

        var response = await next();

        var responseJson = JsonSerializer.Serialize(response);

        _logger.LogInformation("Response for {CorrelationID}: {Response}", correlationIdLog, responseJson);


        return response;
    }
}
}
