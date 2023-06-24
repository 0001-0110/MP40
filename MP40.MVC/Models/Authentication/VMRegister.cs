using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MP40.MVC.Models.Authentication
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [Obsolete]
    public class VMRegister
    {
        [DisplayName("User name")]
        public string Username { get; set; }

        [DisplayName("E-mail")]
        [EmailAddress]
        public string Email { get; set; }
        [DisplayName("Confirm e-mail")]
        [Compare("Email")]
        public string Email2 { get; set; }

        [DisplayName("First name")]
        public string FirstName { get; set; }

        [DisplayName("Last name")]
        public string LastName { get; set; }

        [DisplayName("Password")]
        public string Password { get; set; }
        [DisplayName("Repeat password")]
        [Compare("Password")]
        public string Password2 { get; set; }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
