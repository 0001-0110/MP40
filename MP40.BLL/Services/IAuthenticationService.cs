using MP40.BLL.Models.Authentication;

namespace MP40.BLL.Services
{
    public interface IAuthenticationService
    {
        bool TryAuthenticate(Credentials credentials, out Tokens? tokens);
    }
}
