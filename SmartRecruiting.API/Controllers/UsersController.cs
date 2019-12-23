using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartRecruiting.Application.Users;

namespace SmartRecruiting.API.Controllers {
    public class UsersController : BaseController {

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand request, CancellationToken cancellationToken) {
            return Ok(await Mediator.Send(request, cancellationToken));
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserQuery request, CancellationToken cancellationToken) {
            return Ok(await Mediator.Send(request, cancellationToken));
        }

        
    }
}