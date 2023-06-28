namespace MP40.BLL.Services
{
	public interface ISecurityService
	{
		public string GenerateToken();

		public string GetHash(string password, string salt);

		public string GetHash(string password, byte[] salt);

		public string GetHash(string password, out string salt);

		public string GetSecurityToken();
	}
}
