namespace MP40.BLL.Models
{
    // Should this be in the authentication ?
    public partial class User : IBllModel
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string Username { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PwdHash { get; set; } = null!;

        public string PwdSalt { get; set; } = null!;

        public string? Phone { get; set; }

        public bool IsConfirmed { get; set; }

        public string? SecurityToken { get; set; }

        public int CountryOfResidenceId { get; set; }

        public virtual Country CountryOfResidence { get; set; } = null!;
    }
}
