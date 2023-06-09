﻿namespace MP40.DAL.Models
{
    public partial class VideoTag : IDalModel
    {
        public int Id { get; set; }

        public int VideoId { get; set; }

        public int TagId { get; set; }

        public virtual Tag Tag { get; set; } = null!;

        public virtual Video Video { get; set; } = null!;
    }
}