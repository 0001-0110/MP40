using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MP40.BLL.Models.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Task04.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        // TODO Check if login is valid
        [HttpPost]
        public ActionResult<Tokens> GetTokens()
        {
            // Get secret key bytes
            byte[] tokenKey = Encoding.UTF8.GetBytes("JWT:TokenSecretKey");

            // Create a token descriptor (represents a token, kind of a "template" for token)
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            // Create token using that descriptor, serialize it and return it
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string serializedToken = tokenHandler.WriteToken(token);

            return new Tokens
            {
                Token = serializedToken
            };
        }
    }
}
