namespace MP40.BLL.Models
{
    public partial class Tag : IBllModel, INamedModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<VideoTag> VideoTags { get; set; } = new List<VideoTag>();
    }
}