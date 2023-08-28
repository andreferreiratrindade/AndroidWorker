using System.Net;
using Microsoft.AspNetCore.Mvc;
using Framework.WebApi.Core.Controllers;
using Framework.Core.Mediator;
using Activities.Application.Commands.AddActivity;
using Activities.Application.Commands.DeleteActivity;
using Framework.Core.Messages;
using Activities.Application.Commands.UpdateTimeStartAndTimeEndActivity;
using Activities.Domain.DTO;
using Activities.Application.Queries;
using NSwag.Annotations;

namespace Activities.Api.Controllers
{
  [ApiController]
  [Route("api/v1/activity")]
  [OpenApiTag("Activity workers", Description = "Activity workers services")]
  public class ActivityController : MainController
  {
    private readonly IMediatorHandler _mediatorHandler;
    private readonly IActivityQuery _activityQuery;
    public ActivityController(IMediatorHandler mediatorHandler, IActivityQuery activityQuery)
    {
      _mediatorHandler = mediatorHandler;
      _activityQuery = activityQuery;
    }

        /// <summary>
        /// Add an activity for worker
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
    [ProducesResponseType(
  typeof(AddActivityCommandOutput),
  (int)HttpStatusCode.Created)]
    [ProducesResponseType(
  typeof(ValidationProblemDetails),
  (int)HttpStatusCode.Conflict)]
    [ProducesResponseType(
  typeof(ValidationProblemDetails),
  (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(
        typeof(ValidationProblemDetails),
        (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> AddActivityAsync([FromBody]AddActivityCommand command)
    {
      var commandHandlerOutput = await _mediatorHandler.SendCommand<AddActivityCommand, AddActivityCommandOutput>(command);
      return CustomResponseStatusCodeCreated(commandHandlerOutput, $"activity/{commandHandlerOutput.ActivityId}");
    }

    /// <summary>
    /// Update start and end time of an activity
    /// </summary>
    /// <returns></returns>
    [HttpPatch]
    [ProducesResponseType(
      typeof(UpdateTimeStartAndTimeEndActivityCommandOutput),
      (int)HttpStatusCode.OK)]
    [ProducesResponseType(
      typeof(ValidationProblemDetails),
      (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(
      typeof(ValidationProblemDetails),
      (int)HttpStatusCode.Conflict)]
    [ProducesResponseType(
        typeof(ValidationProblemDetails),
        (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> AddActivityAsync( [FromBody]UpdateTimeStartAndTimeEndActivityCommand command)
    {
      return CustomResponseStatusCodeOk(
          await _mediatorHandler.SendCommand<UpdateTimeStartAndTimeEndActivityCommand, UpdateTimeStartAndTimeEndActivityCommandOutput>(command));
    }

    /// <summary>
    /// Delete an activity
    /// </summary>
    /// <param name="activityId"></param>
    /// <returns></returns>
    [HttpDelete("{activityId}")]
    [ProducesResponseType(
  typeof(Result),
  (int)HttpStatusCode.OK)]
    [ProducesResponseType(
  typeof(ValidationProblemDetails),
  (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(
  typeof(ValidationProblemDetails),
  (int)HttpStatusCode.Conflict)]
    [ProducesResponseType(
        typeof(ValidationProblemDetails),
        (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> DeleteActivityAsync(Guid activityId)
    {

      return CustomResponseStatusCodeOk(await _mediatorHandler.SendCommand<DeleteActivityCommand, Result>(new DeleteActivityCommand(activityId)));
    }

    /// <summary>
    /// Get an activity
    /// </summary>
    /// <param name="activityId"></param>
    /// <returns></returns>

    [HttpGet("{activityId}")]
    [ProducesResponseType(
        typeof(ActivityDto),
        (int)HttpStatusCode.OK)]
    [ProducesResponseType(
        typeof(ValidationProblemDetails),
        (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(
        typeof(ValidationProblemDetails),
        (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(
        typeof(ValidationProblemDetails),
        (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetByActivityId(Guid activityId)
    {

      return CustomResponseStatusCodeOk(await _activityQuery.GetActivityById(activityId));

    }

  }
}