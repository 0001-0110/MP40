using System.ComponentModel;

namespace MP40.BLL.Models.Authentication
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class Credentials
    {
		[DisplayName("User name")]
		public string Username { get; set; }

		[DisplayName("Password")]
		public string Password { get; set; }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
