using Application.Features.Users.Commands;
using Application.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediator.Send(new GetAllUsers());
            return Ok(result);
        }

        [HttpGet]
        [Route("GetUserById/{UserId}")]
        public async Task<IActionResult> GetUserById(int UserId)
        {
            var result = await _mediator.Send(new GetUserById { UserID = UserId });
            return Ok(result);
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUser createUser, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(createUser, cancellationToken);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UpdateUser updateUser, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(updateUser, cancellationToken);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteUser/{UserId}")]
        public async Task<IActionResult> DeleteUser(int UserId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteUser { UserID = UserId }, cancellationToken);
            return Ok(result);
        }
    }
}