namespace MP40.BLL.Models
{
    public partial class Genre : IBllModel, INamedModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
