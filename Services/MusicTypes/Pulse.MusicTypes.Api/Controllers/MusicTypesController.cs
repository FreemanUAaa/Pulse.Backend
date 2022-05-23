using Pulse.MusicTypes.Application.Handlers.MusicTypes.Commands.CreateMusicType;
using Pulse.MusicTypes.Application.Handlers.MusicTypes.Commands.DeleteMusicType;
using Pulse.MusicTypes.Application.Handlers.MusicTypes.Commands.UpdateMusicType;
using Pulse.MusicTypes.Application.Handlers.MusicTypes.Queries.GetMusicTypeList;
using Pulse.MusicTypes.Api.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Pulse.MusicTypes.Api.Controllers
{
    public class MusicTypesController : BaseController
    {
        [HttpGet]
        [ApiVersion("1.0")]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(GetMusicTypeListQueryVm), 200)]
        public async Task<ActionResult<GetMusicTypeListQueryVm>> GetList([FromQuery] GetMusicTypeListQuery query) =>
            Ok(await Mediator!.Send(query));

        [HttpPost]
        [ApiVersion("1.0")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(Guid), 200)]
        public async Task<ActionResult<Guid>> Create([FromForm] CreateMusicTypeCommand command) =>
            Ok(await Mediator!.Send(command));

        [HttpPut]
        [ApiVersion("1.0")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult> Update([FromForm] UpdateMusicTypeCommand command) =>
            Ok(await Mediator!.Send(command));

        [HttpDelete]
        [ApiVersion("1.0")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult> Delete([FromForm] DeleteMusicTypeCommand command) =>
            Ok(await Mediator!.Send(command));
    }
}
