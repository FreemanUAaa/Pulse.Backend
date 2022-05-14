using Microsoft.AspNetCore.Mvc;
using Pulse.Users.Api.Controllers.Base;
using Pulse.Users.Application.Handlers.Covers.Commands.DeleteCover;
using Pulse.Users.Application.Handlers.Covers.Commands.UpdateCover;
using Pulse.Users.Application.Handlers.Covers.Queries.GetCoverBytes;

namespace Pulse.Users.Api.Controllers
{
    public class CoversController : BaseController
    {
        [ApiVersion("1.0")]
        [HttpGet("{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult> Get([FromRoute] GetCoverBytesQuery query)
        {
            GetCoverBytesVm vm = await Mediator.Send(query);

            return File(vm.Bytes, $"image/{vm.Extension.Replace(".", "")}");
        }

        [HttpPut]
        [ApiVersion("1.0")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult> Update([FromForm] UpdateCoverCommand command) =>
            Ok(await Mediator.Send(command));

        [HttpDelete]
        [ApiVersion("1.0")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult> Delete([FromForm] DeleteCoverCommand command) =>
            Ok(await Mediator.Send(command));
    }
}
