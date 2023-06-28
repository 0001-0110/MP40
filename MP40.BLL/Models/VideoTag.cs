namespace MP40.BLL.Models
{
    public partial class VideoTag : IBllModel
    {
        public int Id { get; set; }

        public int VideoId { get; set; }

        public int TagId { get; set; }

        public virtual Tag Tag { get; set; } = null!;

        public virtual Video Video { get; set; } = null!;
    }
}
