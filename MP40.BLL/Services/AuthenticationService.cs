using MP40.BLL.Models;
using MP40.BLL.Models.Authentication;
using System;
using System.Text.RegularExpressions;

namespace MP40.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private const int PASSWORDMINLENGTH = 6;

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
            return Register(credentials, out _, out _);
        }

        public bool Register(RegisterCredentials credentials, out string errorKey, out string errorMessage)
        {
            errorKey = null!;
            errorMessage = null!;

            // Check that the username is unique
            if (dataService.GetAll<User>().Any(user => user.Username == credentials.Username))
            {
                errorKey = nameof(credentials.Username);
                errorMessage = "Username already taken";
                return false;
            }

            // Should not ever happen, but just in case
            if (credentials.Email != credentials.EmailConfirmation)
            {
                errorKey = nameof(credentials.Email);
                errorMessage = "Email addresses must match";
                return false;
            }

            // If the field is empty, don't check (this field is not necessary)
            // TODO This regex is not perfect
            // Can fail in case of number starting with +00
            if (credentials.Phone != null && !Regex.IsMatch(credentials.Phone, "^(?:(?:\\+|00)\\d{2}[\\s.-]{0,3}(?:\\(0\\)[\\s.-]{0,3})?|0)[1-9](?:(?:[\\s.-]?\\d{2}){4}|\\d{2}(?:[\\s.-]?\\d{3}){2})$"))
            {
                errorKey = nameof(credentials.Phone);
                errorMessage = "This is not a valid phone number";
                return false;
            }

            // Should not ever happen, but just in case
            if (credentials.Password != credentials.PasswordConfirmation)
            {
                errorKey = nameof(credentials.Password);
                errorMessage = "Passwords must match";
                return false;
            }

            if (credentials.Password.Length < PASSWORDMINLENGTH)
            {
                errorKey = nameof(credentials.Password);
                errorMessage = "This password is too short";
                return false;
            }

            string hash = securityService.GetHash(credentials.Password, out string salt);
            User newUser = new()
            {
                CreatedAt = DateTime.Now,
                DeletedAt = null,
                Username = credentials.Username,
                Email = credentials.Email,
                FirstName = credentials.FirstName,
                LastName = credentials.LastName,
                Phone = credentials.Phone,
                CountryOfResidenceId = credentials.CountryId,
                // TODO Replace this line once confirmation is implemented
                IsConfirmed = true,
                //IsConfirmed = false,
                PwdHash = hash,
                PwdSalt = salt,
            };

            User = newUser;
            return dataService.Create(newUser) != -1;
        }
    }
}
