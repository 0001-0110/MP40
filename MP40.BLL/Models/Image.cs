namespace MP40.BLL.Models
{
    public partial class Image : IBllModel
    {
        public int Id { get; set; }

        public string Content { get; set; } = null!;

        public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
    }
}
