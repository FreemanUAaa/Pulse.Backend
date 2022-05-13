using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pulse.Users.Api.Controllers.Base;
using Pulse.Users.Application.Handlers.Users.Commands.CreateUser;
using Pulse.Users.Application.Handlers.Users.Commands.DeleteUser;
using Pulse.Users.Application.Handlers.Users.Commands.UpdateUser;
using Pulse.Users.Application.Handlers.Users.Queries.GetUserDetails;

namespace Pulse.Users.Api.Controllers
{
    public class UsersController : BaseController
    {
        [ApiVersion("1.0")]
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(GetUserDetailsVm), 200)]
        public async Task<ActionResult<GetUserDetailsVm>> GetDetails([FromRoute] GetUserDetailsQueryHandler query) =>
            Ok(await Mediator.Send(query));

        [HttpPost]
        [ApiVersion("1.0")]
        [ProducesResponseType(typeof(Guid), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult<Guid>> Create([FromForm] CreateUserCommand command) =>
            Ok(await Mediator.Send(command));

        [HttpPut]
        [ApiVersion("1.0")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult> Update([FromForm] UpdateUserCommand command) =>
            Ok(await Mediator.Send(command));

        [ApiVersion("1.0")]
        [HttpDelete("{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult<Guid>> Delete([FromForm] DeleteUserCommand command) =>
            Ok(await Mediator.Send(command));
    }
}
