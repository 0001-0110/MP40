using MP40.BLL.Models;
using MP40.BLL.Models.Authentication;

namespace MP40.BLL.Services
{
    public interface IAuthenticationService
    {
        User? User { get; }

        /// <summary>
        /// Attempts to authenticate the user using the provided credentials.
        /// </summary>
        /// <param name="credentials">The user's login credentials.</param>
        /// <param name="tokens">The generated authentication tokens if the authentication is successful.</param>
        /// <returns><c>true</c> if the authentication is successful; otherwise, <c>false</c>.</returns>
        bool TryAuthenticate(Credentials credentials, out Tokens tokens);

        /// <summary>
        /// Attempts to authenticate the user using the provided credentials.
        /// </summary>
        /// <param name="credentials">The user's login credentials.</param>
        /// <returns><c>true</c> if the authentication is successful; otherwise, <c>false</c>.</returns>
        bool TryAuthenticate(Credentials credentials);

        bool Register(RegisterCredentials credentials, out string errorKey, out string errorMessage);
    }
}
