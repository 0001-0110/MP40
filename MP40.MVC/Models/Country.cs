namespace MP40.MVC.Models
{
    public partial class Country : IViewModel
    {
        public int Id { get; set; }

        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
