using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Pulse.MusicTypes.Api.Controllers.Base
{
    [ApiController]
    [Route("api/{v:apiVersion}/[controller]")]
    public class BaseController : Controller
    {
        public IMediator? Mediator => HttpContext.RequestServices.GetService(typeof(IMediator)) as IMediator;
    }
}
