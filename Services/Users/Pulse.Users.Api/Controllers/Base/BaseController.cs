using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Pulse.Users.Api.Controllers.Base
{
    [ApiController]
    [Route("api/{v:apiVersion}/[controller]")]
    public class BaseController : Controller
    {
        public IMediator Mediator => HttpContext.RequestServices.GetService(typeof(IMediator)) as IMediator;

        public Guid UserId => Guid.Parse(HttpContext.User.Identity?.Name ?? string.Empty);
    }
}
