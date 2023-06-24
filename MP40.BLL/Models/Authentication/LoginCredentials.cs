using System.ComponentModel;

namespace MP40.BLL.Models.Authentication
{
    public class LoginCredentials : Credentials
	{
		[DisplayName("Stay Signed-in")]
		public bool StaySignedIn { get; set; }
	}
}
