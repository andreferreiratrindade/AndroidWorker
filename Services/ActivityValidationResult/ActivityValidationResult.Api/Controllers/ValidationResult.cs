using ActivityValidationResult.Application.Commands.AddActivityValidationResult;
using Framework.Core.Mediator;
using Framework.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Net;

namespace ActivityValidationResult.Api.Controllers
{
    [ApiController]
    [Route("api/v1/activity")]
    [OpenApiTag("Activity workers", Description = "Activity workers services")]
    public class ValidationResultController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;
        public ValidationResultController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }  /// <summary>
           /// Add an activity for worker
           /// </summary>
           /// <param name="command"></param>
           /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(
  typeof(AddActivityValidationResultCommandOutput),
  (int)HttpStatusCode.Accepted)]
        [ProducesResponseType(
  typeof(ValidationProblemDetails),
  (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(
        typeof(ValidationProblemDetails),
        (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AddActivityAsync([FromBody] AddActivityValidationResultCommand command)
        {
            var commandHandlerOutput = await _mediatorHandler.SendCommand<AddActivityValidationResultCommand, AddActivityValidationResultCommandOutput>(command);
            return CustomResponseStatusCodeAccepted(commandHandlerOutput, $"activity/");
        }
    }
}
