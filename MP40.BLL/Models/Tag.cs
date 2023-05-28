namespace MP40.BLL.Models
{
    public partial class Tag : IBllModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<Video> VideoTags { get; set; } = new List<Video>();
    }
}
