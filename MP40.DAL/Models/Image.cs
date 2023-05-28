namespace MP40.DAL.Models;

public partial class Image : IDalModel
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
}
