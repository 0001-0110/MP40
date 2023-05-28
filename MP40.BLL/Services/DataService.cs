using MP40.BLL.Models;

namespace MP40.BLL.Services
{
    public class DataService : IDataService
    {
        #region Generics

        public IEnumerable<T> GetAll<T>() where T : IBllModel
        {
            throw new NotImplementedException();
        }

        public T? GetById<T>(int id) where T : IBllModel
        {
            throw new NotImplementedException();
        }

        public bool Create<T>(T model) where T : IBllModel
        {
            throw new NotImplementedException();
        }

        public bool Edit<T>(int id, T model) where T : IBllModel
        {
            throw new NotImplementedException();
        }

        public bool Delete<T>(int id) where T : IBllModel
        {
            throw new NotImplementedException();
        }

        #endregion

        public IEnumerable<Video>? SearchVideos(int page = 0, int pageSize = 0, string? name = null, string? orderedBy = null)
        {
            IEnumerable<Video> videos = GetAll<Video>();

            if (page > 0 && pageSize > 0)
            {
                // The first page is page 1
                videos = videos.Skip((page - 1) * pageSize).Take(pageSize);
            }

            if (name != null)
                // Filter the videos, ignoring the case
                videos.Where(video => video.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

            if (orderedBy != null)
            {
                Func<Video, object>? ordering = orderedBy switch
                {
                    "id" => video => video.Id,
                    "name" => Video => Video.Name,
                    "total_time" => Video => Video.TotalSeconds,
                    _ => null,
                };
                // This ordering is not recognized
                if (ordering == null)
                    return null;
                videos = videos.OrderBy(ordering);
            }

            return videos;
        }
    }
}
