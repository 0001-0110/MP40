using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace MP40.BLL.Services
{
	public class SecurityService : ISecurityService
	{
		public class HashFunction
		{
			public readonly Func<string, byte[], byte[]> GetHash;

			public HashFunction(Func<string, byte[], byte[]> getHash)
			{
				GetHash = getHash;
			}
		}

		private readonly HashFunction hashFunction;

		public SecurityService(HashFunction hashFunction) 
		{
			this.hashFunction = hashFunction;
		}

		#region JWT

		public string GenerateToken()
		{
			// Get secret key bytes
			byte[] tokenKey = Encoding.UTF8.GetBytes("JWT:TokenSecretKey");

			// Create a token descriptor (represents a token, kind of a "template" for token)
			SecurityTokenDescriptor tokenDescriptor = new()
			{
				Expires = DateTime.UtcNow.AddMinutes(10),
				SigningCredentials = new SigningCredentials(
					new SymmetricSecurityKey(tokenKey),
					SecurityAlgorithms.HmacSha256Signature)
			};

			// Create token using that descriptor, serialize it and return it
			JwtSecurityTokenHandler tokenHandler = new();
			SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
			// Serialize token
			return tokenHandler.WriteToken(token);
		}

		#endregion

		private byte[] GenerateSalt()
		{
			return RandomNumberGenerator.GetBytes(128 / 8);
        }

		public string GetHash(string password, string salt)
		{
			return GetHash(password, Convert.FromBase64String(salt));
		}

		public string GetHash(string password, byte[] salt)
		{
			return Convert.ToBase64String(hashFunction.GetHash(password, salt));
		}

		public string GetHash(string password, out string salt)
		{
			byte[] byteSalt = GenerateSalt();
			salt = Convert.ToBase64String(byteSalt);
			return GetHash(password, byteSalt);
		}
	}
}
