using Framework.Core.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Framework.Core.DomainObjects;
using MediatR;
using Newtonsoft.Json;
using Framework.Core.Messages;

namespace Framework.WebApi.Core.Filters
{

    public class DomainNotificationFilter : IAsyncResultFilter
    {
        private readonly IDomainNotification _domainNotification;
        //public TelemetryClient _telemetry;

        public DomainNotificationFilter(IDomainNotification domainNotification)
        {
            _domainNotification = domainNotification;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (_domainNotification.HasNotifications)
            {
                var problemDetails = new ValidationProblemDetails()
                {
                    Instance = context.HttpContext.Request.Path,
                    Status = StatusCodes.Status409Conflict,
                    Detail = "Please refer to the errors property for additional details."
                };

               
                problemDetails.Errors.Add("DomainValidations", _domainNotification.Notifications.Select(x => x.Value).ToArray());

                //context.Result = problemDetails;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
                context.HttpContext.Response.ContentType = "application/problem+json";

                var notifications = JsonConvert.SerializeObject(problemDetails);
                await context.HttpContext.Response.WriteAsync(notifications);
                return;
            }

            await next();
        }
    }
}
