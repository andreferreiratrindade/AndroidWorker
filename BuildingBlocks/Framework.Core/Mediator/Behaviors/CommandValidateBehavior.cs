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
        private ILogger _logger;
        protected readonly IMediatorHandler _mediatorHandler;


        public CommandValidateBehavior(IMediatorHandler mediatorHandler,
                                        IDomainNotification domainNotification,
                                        ILogger<RequestResponseLoggingBehavior<TRequest, TResponse>> logger)
        {
            _domainNotification = domainNotification;
            _mediatorHandler = mediatorHandler;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            if (request.GetValidationResult() == null
                || request.GetCommandOutput() == null
                || request.GetRollBackEvent() == null)
                throw new Exception("You need to add ValidationResult or CommandOutput in your class command's constructor, doing something like that:    this.AddValidCommand(new AddActivityCommandValidation().Validate(this)); this.AddCommandOutput(new AddActivityCommandOutput()); ");

            _domainNotification.AddNotifications(request.GetValidationResult());

            var response = await CheckNotifications(request, default);

            if (response != null) return response;

            response = await next();

            response = await CheckNotifications(request, response);

            return response;
        }

        private async Task<TResponse> CheckNotifications(TRequest request, TResponse response)
        {

            if (_domainNotification.HasNotifications)
            {
                var notificationsJson = JsonSerializer.Serialize(_domainNotification.Notifications);

                _logger.LogInformation(notificationsJson);

                var @event = request.GetRollBackEvent();
                @event.AddNotifications(_domainNotification.Notifications.ToList());

                await _mediatorHandler.PublishEvent(@event);

                response = request.GetCommandOutput();
            }

            return response;
        }
    }
}
