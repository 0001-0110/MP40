﻿namespace MP40.BLL.Models
{
    public partial class Genre : IBllModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
    }
}