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
  [Route("api/v1/dash-board")]
  [OpenApiTag("DashBoard - Workers", Description = "DashBoard of workers")]

  public class DashBoardController : MainController
  {
    private readonly IMediatorHandler _mediatorHandler;

    public DashBoardController(IMediatorHandler mediatorHandler)
    {
      _mediatorHandler = mediatorHandler;
    }

/// <summary>
/// Top 10 androids that are working more time in the next 7 days
/// </summary>
/// <returns></returns>
    [HttpGet("WorkersActiveNext7Days")]
    [ProducesResponseType(
    typeof(List<WorkActiveReportDto>),
    (int)HttpStatusCode.OK)]
    [ProducesResponseType(
    typeof(ValidationProblemDetails),
    (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(
    typeof(ValidationProblemDetails),
    (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetWorkersActiveNext7Days()
    {

      return CustomResponseStatusCodeOk(await _mediatorHandler.Send(new GetWorkersActiveNext7DaysQuery(DateTime.Now)));

    }


  }
}