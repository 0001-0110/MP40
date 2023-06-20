using System.ComponentModel;

namespace MP40.MVC.Models
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class VMLogin
    {
        [DisplayName("User name")]
        public string Username { get; set; }

        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("Stay Signed-in")]
        public bool StaySignedIn { get; set; }

        public string? RedirectUrl { get; set; }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
