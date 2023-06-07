using Microsoft.AspNetCore.Mvc;
using MP40.BLL.Models.Authentication;
using MP40.BLL.Services;

namespace MP40.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public TokenController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost]
        public ActionResult<Tokens> GetTokens([FromBody] Credentials credentials)
        {
            Tokens? tokens;
            if (!authenticationService.TryAuthenticate(credentials, out tokens))
                return Unauthorized(new { error = "Invalid credentials" });
            return Ok(tokens);
        }
    }
}
