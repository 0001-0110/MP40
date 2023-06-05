using MP40.DAL.Models;
using MP40.DAL.Repositories;

namespace MP40.BLL.Services
{
    public class DataService : IDataService
    {
        private IRepositoryCollection repositoryCollection;

        public DataService(IRepositoryCollection repositoryCollection)
        {
            this.repositoryCollection = repositoryCollection;
        }

        #region Generics

        public IEnumerable<T> GetAll<T>() where T : class, IModel
        {
            return repositoryCollection.GetRepository<T>()!.GetAll();
        }

        public T? GetById<T>(int id) where T : class, IModel
        {
            return repositoryCollection.GetRepository<T>()!.GetById(id);
        }

        public bool Create<T>(T model) where T : class, IModel
        {
            repositoryCollection.GetRepository<T>()!.Create(model);
            return true;
        }

        public bool Edit<T>(int id, T model) where T : class, IModel
        {
            repositoryCollection.GetRepository<T>()!.Edit(id, model);
            return true;
        }

        public bool Delete<T>(int id) where T : class, IModel
        {
            T? entity = GetById<T>(id);
            if (entity == null)
                return false;
            repositoryCollection.GetRepository<T>()!.Delete(entity);
            return true;
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
