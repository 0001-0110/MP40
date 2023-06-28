using Microsoft.AspNetCore.Mvc;
using MP40.BLL.Models;
using MP40.BLL.Models.Authentication;
using MP40.BLL.Services;

namespace MP40.Controllers
{
    [ApiController]
	[Route("api/users")]
	public class UserController : ControllerBase
    {
        private readonly ILogger<ModelController<User>> logger;
        private readonly IAuthenticationService authenticationService;

        public UserController(ILogger<ModelController<User>> logger, IAuthenticationService authenticationService)
        {
            this.logger = logger;
            this.authenticationService = authenticationService;
        }

        [HttpPost("[action]")]
        public ActionResult<RegisterCredentials> Register(RegisterCredentials credentials)
        {
            if (!authenticationService.Register(credentials))
                return BadRequest();
            return Ok(credentials);
        }

        [HttpPost("[action]")]
        public ActionResult Confirm(string securityToken)
        {
            if (!authenticationService.Confirm(securityToken))
                return NotFound();
            return Ok();
        }
    }
}
