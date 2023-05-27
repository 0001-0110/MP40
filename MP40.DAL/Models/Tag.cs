namespace MP40.DAL.Models;

public partial class Tag : IModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<VideoTag> VideoTags { get; set; } = new List<VideoTag>();
}
