using MP40.DAL.DataBaseContext;
using MP40.DAL.Models;

namespace MP40.DAL.Repositories
{
    internal class VideoRepository : Repository<Video>
    {
        public VideoRepository(RwaMoviesContext rwaMoviesContext) : base(rwaMoviesContext) { }

        public override IEnumerable<Video> GetAll()
        {
            return dbContext.Videos;
        }

        public override Video? GetById(int id)
        {
            return dbContext.Videos.SingleOrDefault(video => video.Id == id);
        }

        public override void Create(Video entity)
        {
            dbContext.Videos.Add(entity);
            dbContext.SaveChanges();
        }

        public override void Edit(Video entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Video entity)
        {
            dbContext.Remove(entity);
            dbContext.SaveChanges();
        }
    }
}
