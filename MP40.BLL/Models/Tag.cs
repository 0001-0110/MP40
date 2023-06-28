namespace MP40.BLL.Models
{
    public partial class Tag : IBllModel, INamedModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}