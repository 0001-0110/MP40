namespace MP40.MVC.Models
{
    public partial class Image : IViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; } = null!;

        public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
    }
}