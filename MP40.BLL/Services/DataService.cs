using MP40.BLL.Mapping;
using MP40.BLL.Models;
using MP40.BLL.Models.Authentication;
using MP40.DAL.Repositories;
using MP40.DAL.Extensions;

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

        private void InvokeRepository<TModel>(string methodName, params object[] arguments) where TModel : class, IBllModel
        {
            InvokeRepository<TModel, object>(methodName, arguments);
        }

        private TResult? InvokeRepository<TModel, TResult>(string methodName, params object[] arguments) where TModel : class, IBllModel
        {
            Type modelType = mapper.GetMappedType(typeof(TModel))!;
            object? repository = repositoryCollection.GetRepository(modelType);
            return (TResult?)repository?.CallMethod(methodName, arguments);
		}

        public IEnumerable<object>? GetAll(Type type)
        {
            return (IEnumerable<object>?)this.CallGeneric(nameof(GetAll), type);
        }

		public IEnumerable<T> GetAll<T>() where T : class, IBllModel
        {
            IEnumerable<object>? result = InvokeRepository<T, IEnumerable<object>>("GetAll")!;
            return mapper.MapRange<T>(result);
        }

        public IEnumerable<object>? GetWhere(Type type, object predicate)
        {
            return (IEnumerable<object>?)this.CallGeneric(nameof(GetWhere), type, predicate);
        }

        public IEnumerable<T> GetWhere<T>(Predicate<T> predicate) where T : class, IBllModel
        {
            IEnumerable<object>? result = InvokeRepository<T, IEnumerable<object>>("GetWhere", predicate)!;
            return mapper.MapRange<T>(result);
        }

        public object? GetById(Type type, int id)
        {
            return this.CallGeneric(nameof(GetById), type, id);
        }

        public T? GetById<T>(int id) where T : class, IBllModel
        {
            object? result = InvokeRepository<T, object>("GetById", id);
            return mapper.Map<T>(result);
        }

        public bool Create(Type type, object model)
        {
            return (bool)this.CallGeneric(nameof(Create), type, model)!;
        }

        public bool Create<T>(T model) where T : class, IBllModel
        {
            Type mappedType = mapper.GetMappedType(typeof(T));
            InvokeRepository<T>("Create", mapper.Map(mappedType, model)!);
            return true;
        }

        public bool Edit(Type type, int id, object model)
        {
            return (bool)this.CallGeneric(nameof(Edit), type, id, model)!;
        }

        public bool Edit<T>(int id, T model) where T : class, IBllModel
        {
            Type mappedType = mapper.GetMappedType(typeof(T));
            InvokeRepository<T>("Edit", id, mapper.Map(mappedType, model)!);
            return true;
        }

        public bool Delete(Type type, int id)
        {
            return (bool)this.CallGeneric(nameof(Delete), type, id)!;
        }

        public bool Delete<T>(int id) where T : class, IBllModel
        {
            InvokeRepository<T>("Delete", id);
            return true;
        }

        #endregion

        public User? GetUser(Credentials credentials)
        {
            Func<DAL.Models.User, bool> predicate = user =>
                user.Username == credentials.Username
                // TODO Handle password hashing
                && user.PwdHash == credentials.Password
                && user.IsConfirmed
                && !user.DeletedAt.HasValue;
            // EntityFramework does not like when I call getWhere directly from here, I have no idea why
            DAL.Models.User? user = InvokeRepository<User, IEnumerable<DAL.Models.User>>("GetAll")?.Where(predicate).SingleOrDefault();
            //DAL.Models.User? user = InvokeRepository<User, IEnumerable<DAL.Models.User>>("GetWhere", predicate)?.SingleOrDefault();
            return mapper.Map<User>(user);
        }

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
