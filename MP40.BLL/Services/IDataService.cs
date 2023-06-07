using MP40.BLL.Models;
using MP40.BLL.Models.Authentication;

namespace MP40.BLL.Services
{
    public interface IDataService
    {
        IEnumerable<T> GetAll<T>() where T : class, IBllModel;

        IEnumerable<T> GetWhere<T>(Predicate<T> predicate) where T : class, IBllModel;

        T? GetById<T>(int id) where T : class, IBllModel;

        bool Create<T>(T model) where T : class, IBllModel;

        bool Edit<T>(int id, T model) where T : class, IBllModel;

        bool Delete<T>(int id) where T : class, IBllModel;

        User? GetUser(Credentials credentials);

        IEnumerable<Video>? SearchVideos(int page = 0, int pageSize = 0, string? name = null, string? orderedBy = null);
    }
}
