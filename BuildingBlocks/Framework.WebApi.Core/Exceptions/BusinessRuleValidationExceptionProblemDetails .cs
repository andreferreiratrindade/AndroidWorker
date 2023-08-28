using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Core.DomainObjects;
using Microsoft.AspNetCore.Http;

namespace Framework.WebApi.Core.Exceptions
{
  public class BusinessRuleValidationExceptionProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
{
    public BusinessRuleValidationExceptionProblemDetails(BusinessRuleValidationException exception)
    {
        this.Title = exception.Message;
        this.Status = StatusCodes.Status409Conflict;
        this.Detail = exception.Details;
        this.Type = "https://somedomain/business-rule-validation-error";
    }
}
}