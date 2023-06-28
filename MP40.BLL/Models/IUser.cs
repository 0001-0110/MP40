namespace MP40.BLL.Models
{
	internal interface IUser : INamedModel
	{
		string FirstName { get; }

		string LastName { get; }

		int CountryId { get; }
	}
}
