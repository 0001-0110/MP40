using MP40.BLL.Mapping;
using MP40.BLL.Models;
using MP40.DAL.Repositories;

namespace MP40.BLL.Services
{
    public class DataService : IDataService
    {
        // TODO
        private IBijectiveMapper<BllMapperProfile> mapper;
        private IRepositoryCollection repositoryCollection;

        public DataService(IBijectiveMapper<BllMapperProfile> mapper, IRepositoryCollection repositoryCollection)
        {
            this.mapper = mapper;
            this.repositoryCollection = repositoryCollection;
        }

        #region Generics

        private void InvokeRepository<TModel>(string methodName, params object[]? arguments) where TModel : class, IBllModel
        {
            InvokeRepository<TModel, object>(methodName, arguments);
        }

        private TResult? InvokeRepository<TModel, TResult>(string methodName, params object[]? arguments) where TModel : class, IBllModel
        {
            Type modelType = mapper.GetMappedType(typeof(TModel))!;
            object? repository = repositoryCollection.GetRepository(modelType);
            return (TResult?)repository?.GetType().GetMethod(methodName)?.Invoke(repository, arguments);
        }

        public IEnumerable<T> GetAll<T>() where T : class, IBllModel
        {
            IEnumerable<object>? result = InvokeRepository<T, IEnumerable<object>>("GetAll")!;
            return mapper.MapRange<T>(result);
        }

        public T? GetById<T>(int id) where T : class, IBllModel
        {
            object? result = InvokeRepository<T, object>("GetById", id);
            return mapper.Map<T>(result);
        }

        public bool Create<T>(T model) where T : class, IBllModel
        {
            InvokeRepository<T>("Create", model);
            throw new NotImplementedException();
        }

        public bool Edit<T>(int id, T model) where T : class, IBllModel
        {
            InvokeRepository<T>("Edit", id, model);
            throw new NotImplementedException();
        }

        public bool Delete<T>(int id) where T : class, IBllModel
        {
            /*T? entity = GetById<T>(id);
            if (entity == null)
                return false;
            repositoryCollection.GetRepository<T>()!.Delete(entity);
            return true;*/
            InvokeRepository<T>("Delete", id);
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
