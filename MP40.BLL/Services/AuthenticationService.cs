using MP40.BLL.Models;
using MP40.BLL.Models.Authentication;
using System;

namespace MP40.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ISecurityService securityService;
        private readonly IDataService dataService;

        public User? User { get; private set; }

        public AuthenticationService(ISecurityService securityService, IDataService dataService)
        {
            this.securityService = securityService;
            this.dataService = dataService;
        }

        private Tokens GenerateToken(Credentials credentials)
        {
            return new Tokens
            {
                Username = credentials.Username,
                Token = securityService.GenerateToken(),
            };
        }

        public bool TryAuthenticate(Credentials credentials, out Tokens tokens)
        {
            // If the method return false, there is no need for a token
            tokens = null!;

            if (!TryAuthenticate(credentials))
                return false;

            tokens = GenerateToken(credentials);
            return true;
        }

        public bool TryAuthenticate(Credentials credentials)
        {
            bool predicate(User user) =>
                user.Username == credentials.Username
                // TODO Handle password hashing
                && user.PwdHash == securityService.GetHash(credentials.Password, user.PwdSalt)
                && user.IsConfirmed
                && !user.DeletedAt.HasValue;

            User = dataService.GetAll<User>().Where(predicate).SingleOrDefault();
            return User != null;
        }

        public bool Register(RegisterCredentials credentials)
        {
            // Check that the username is unique
            if (dataService.GetAll<User>().Any(user => user.Username == credentials.Username))
                return false;

            // Should not be useful, but just in case
            if (credentials.Email != credentials.EmailConfirmation)
                return false;

            // Should not be useful, but just in case
            if (credentials.Password != credentials.PasswordConfirmation)
                return false;

            string salt;
            string hash = securityService.GetHash(credentials.Password, out salt);

            User newUser = new()
            {
                CreatedAt = DateTime.UtcNow,
                DeletedAt = null,
                Username = credentials.Username,
                Email = credentials.Email,
                FirstName = credentials.FirstName,
                LastName = credentials.LastName,
                CountryOfResidenceId = credentials.CountryId,
                // TODO Replace this line once confirmation is implemented
                IsConfirmed = true,
                //IsConfirmed = false,
                PwdHash = hash,
                PwdSalt = salt,
            };

            return dataService.Create(newUser);
        }
    }
}
