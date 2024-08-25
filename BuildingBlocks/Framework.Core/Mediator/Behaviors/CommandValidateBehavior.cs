using Framework.Core.Messages;
using Framework.Core.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Framework.Core.Mediator
{

public class CommandValidateBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : Command<TResponse>
{
    private IDomainNotification _domainNotification;
    public CommandValidateBehavior(IDomainNotification domainNotification)
    {
        _domainNotification = domainNotification;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if(request.GetValidationResult() == null || request.GetCommandOutput() == null) throw new Exception("You need to add ValidationResult or CommandOutput in your class command's constructor, doing something like that:    this.AddValidCommand(new AddActivityCommandValidation().Validate(this)); this.AddCommandOutput(new AddActivityCommandOutput()); ");
        _domainNotification.AddNotifications(request.GetValidationResult());

        if(_domainNotification.HasNotifications) return request.GetCommandOutput();

        var response = await next();

        return response;
    }
}
}
