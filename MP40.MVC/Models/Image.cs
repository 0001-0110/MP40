namespace MP40.MVC.Models
{
    public partial class Image : IViewModel
    {
        public int Id { get; set; }

        public IFormFile Content { get; set; } = null!;

        public string? Base64 { get; set; }

        public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
    }
}