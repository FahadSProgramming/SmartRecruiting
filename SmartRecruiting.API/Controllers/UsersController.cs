using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartRecruiting.Application.Users;

namespace SmartRecruiting.API.Controllers {
    public class UsersController : BaseController {

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommand request, CancellationToken cancellationToken) {
            return Ok(await Mediator.Send(request, cancellationToken));
        }
    }
}