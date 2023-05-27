using Microsoft.EntityFrameworkCore;
using MP40.DAL.Models;

namespace MP40.DAL.DataBaseContext;

public partial class RwaMoviesContext : DbContext
{
    public RwaMoviesContext() { }

    public RwaMoviesContext(DbContextOptions<RwaMoviesContext> options) : base(options) { }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    public virtual DbSet<VideoTag> VideoTags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Country");

            entity.Property(country => country.Code)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();

            entity.Property(country => country.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("Genre");

            entity.Property(genre => genre.Name).HasMaxLength(256);
            entity.Property(genre => genre.Description).HasMaxLength(1024);
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.ToTable("Image");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.ToTable("Notification");

            entity.Property(email => email.ReceiverEmail).HasMaxLength(256);
            entity.Property(email => email.Subject).HasMaxLength(256);
            entity.Property(email => email.Body).HasMaxLength(1024);
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.ToTable("Tag");

            entity.Property(tag => tag.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
            entity.Property(user => user.Email).HasMaxLength(256);
            entity.Property(user => user.FirstName).HasMaxLength(256);
            entity.Property(user => user.LastName).HasMaxLength(256); 
            entity.Property(user => user.Phone).HasMaxLength(256);
            entity.Property(user => user.PwdHash).HasMaxLength(256);
            entity.Property(user => user.PwdSalt).HasMaxLength(256);
            entity.Property(user => user.SecurityToken).HasMaxLength(256);
            entity.Property(user => user.Username).HasMaxLength(50);

            entity.HasOne(user => user.CountryOfResidence).WithMany(country => country.Users)
                .HasForeignKey(country => country.CountryOfResidenceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Country");
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.ToTable("Video");

            entity.Property(video => video.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(video => video.Description).HasMaxLength(1024);
            entity.Property(video => video.Name).HasMaxLength(256);
            entity.Property(video => video.StreamingUrl).HasMaxLength(256);

            entity.HasOne(video => video.Genre).WithMany(genre => genre.Videos)
                .HasForeignKey(genre => genre.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Video_Genre");

            entity.HasOne(video => video.Image).WithMany(image => image.Videos)
                .HasForeignKey(image => image.ImageId)
                .HasConstraintName("FK_Video_Images");
        });

        modelBuilder.Entity<VideoTag>(entity =>
        {
            entity.ToTable("VideoTag");

            entity.HasOne(videoTag => videoTag.Tag).WithMany(tag => tag.VideoTags)
                .HasForeignKey(tag => tag.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VideoTag_Tag");

            entity.HasOne(videoTag => videoTag.Video).WithMany(video => video.VideoTags)
                .HasForeignKey(video => video.VideoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VideoTag_Video");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
