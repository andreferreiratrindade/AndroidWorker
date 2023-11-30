using Microsoft.AspNetCore.Mvc;

namespace Framework.WebApi.Core.Controllers
{
    [ApiController]
    public abstract class MainController : Controller
    {
        protected ICollection<string> Erros = new List<string>();


        protected ActionResult CustomResponseStatusCodeCreated<T> (T commandHandlerOutput, string urlRedirect) where T : class
        {

                var obj = new {data = commandHandlerOutput, Link =urlRedirect};
                return Created(urlRedirect, obj);

        }

        protected ActionResult CustomResponseStatusCodeAccepted<T>(T commandHandlerOutput, string urlRedirect) where T : class
        {

            var obj = new { data = commandHandlerOutput, Link = urlRedirect };
            return Accepted(urlRedirect, obj);

        }

        protected ActionResult CustomResponseStatusCodeOk<T> (T commandHandlerOutput) where T : class
        {
                var obj = new {data = commandHandlerOutput};
                return Ok(obj);
        }
    }
}