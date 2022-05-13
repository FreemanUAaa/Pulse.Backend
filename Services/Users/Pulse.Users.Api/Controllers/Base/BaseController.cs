using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Pulse.Users.Api.Controllers.Base
{
    [ApiController]
    [Route("api/{v:apiVersion}/[controller]")]
    public class BaseController : Controller
    {
        public readonly IMediator Mediator;

        public readonly Guid UserId;

        public BaseController() => 
            (Mediator, UserId) = (HttpContext.RequestServices.GetService(typeof(IMediator)) as IMediator, Guid.Parse(HttpContext.User.Identity?.Name ?? string.Empty));
    }
}
