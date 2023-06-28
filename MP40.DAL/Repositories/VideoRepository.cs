using Microsoft.EntityFrameworkCore;
using MP40.DAL.DataBaseContext;
using MP40.DAL.Models;

namespace MP40.DAL.Repositories
{
    public class VideoRepository : Repository<Video>
    {
        private readonly IRepository<Image> imageRepository;

        public VideoRepository(RwaMoviesContext rwaMoviesContext, IRepository<Image> imageRepository) : base(rwaMoviesContext)
        {
            this.imageRepository = imageRepository;
        }

        public override IEnumerable<Video> GetAll()
        {
            return dbContext.Videos
                .Include(video => video.Genre)
                .Include(video => video.VideoTags)
                .Include(video => video.Image);
        }

        public override Video? GetById(int id)
        {
            return dbContext.Videos
                .Include(video => video.Genre)
                .Include(video => video.VideoTags)
                .Include(video => video.Image)
                .SingleOrDefault(video => video.Id == id);
        }

        public override void Create(Video entity)
        {
            imageRepository.Create(entity.Image!);
            base.Create(entity);
        }
    }
}
