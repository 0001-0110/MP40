using Microsoft.IdentityModel.Tokens;
using MP40.BLL.Models;
using MP40.BLL.Models.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MP40.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private IDataService dataService;

        public AuthenticationService(IDataService dataService)
        {
            this.dataService = dataService;
        }

        private Tokens GenerateToken(User user)
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
                Username = user.Username,
                Token = serializedToken,
            };
        }

        public bool TryAuthenticate(Credentials credentials, out Tokens? tokens)
        {
            tokens = null;
            User? user = dataService.GetUser(credentials);
            if (user == null)
                return false;

            tokens = GenerateToken(user);
            return true;
        }
    }
}
