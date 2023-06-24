using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MP40.BLL.Models.Authentication
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class RegisterCredentials : Credentials
    {
        [DisplayName("E-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Confirm e-mail")]
        [Compare("Email")]
        public string EmailConfirmation { get; set; }

        [DisplayName("First name")]
        public string FirstName { get; set; }

        [DisplayName("Last name")]
        public string LastName { get; set; }

        public int CountryId { get; set; }

        [DisplayName("Confirm password")]
        [Compare("Password")]
        public string PasswordConfirmation { get; set; }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
