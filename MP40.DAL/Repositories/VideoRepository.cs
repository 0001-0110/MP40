using Microsoft.EntityFrameworkCore;
using MP40.DAL.DataBaseContext;
using MP40.DAL.Models;

namespace MP40.DAL.Repositories
{
    public class VideoRepository : Repository<Video>
    {
        private readonly IRepository<Image> imageRepository;
        private readonly IRepository<VideoTag> videoTagRepository;

        public VideoRepository(RwaMoviesContext rwaMoviesContext, IRepositoryCollection repositoryCollection) : base(rwaMoviesContext)
        {
            this.imageRepository = repositoryCollection.GetRepository<Image>()!;
            this.videoTagRepository = repositoryCollection.GetRepository<VideoTag>()!;
        }

        public override IEnumerable<Video> GetAll()
        {
            return dbContext.Videos
                .Include(video => video.Genre)
                .Include(video => video.VideoTags).ThenInclude(videoTag => videoTag.Tag)
				.Include(video => video.Image);
        }

        public override Video? GetById(int id)
        {
            return GetAll().SingleOrDefault(video => video.Id == id);
        }

        public override bool Create(Video entity)
        {
            imageRepository.Create(entity.Image!);
            bool result = base.Create(entity);
            
            entity.VideoTags = entity.TagIds?.Select(tagId => new VideoTag() { VideoId = entity.Id, TagId = tagId}).ToList() ?? new List<VideoTag>();
            foreach (VideoTag videoTag in entity.VideoTags)
                videoTagRepository.Create(videoTag);

            return result;
        }

        public override bool Edit(int id, Video entity)
        {
            Video dbEntity = base.GetById(id)!;

            foreach (VideoTag videoTag in dbEntity.VideoTags)
                videoTagRepository.Delete(videoTag);

            bool result = base.Edit(id, entity);

            entity.VideoTags = entity.TagIds?.Select(tagId => new VideoTag() { VideoId = entity.Id, TagId = tagId }).ToList() ?? new List<VideoTag>();
            foreach (VideoTag videoTag in entity.VideoTags)
                videoTagRepository.Create(videoTag);

            return result;
        }
    }
}
