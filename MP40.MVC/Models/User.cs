namespace MP40.MVC.Models
{
    // Should this be in the authentication ?
    public partial class User : IViewModel
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string Username { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string PasswordConfirmation { get; set; } = null!;

        public string? Phone { get; set; }

        public bool IsConfirmed { get; set; }

        public int CountryOfResidenceId { get; set; }

        public virtual Country CountryOfResidence { get; set; } = null!;
    }
}
