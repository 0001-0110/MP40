using MP40.BLL.Models;

namespace MP40.BLL.Services
{
    public interface IDataService
    {
        public IEnumerable<T> GetAll<T>() where T : IBllModel;

        public T? GetById<T>(int id) where T : IBllModel;

        public bool Create<T>(T model) where T : IBllModel;

        public bool Edit<T>(int id, T model) where T : IBllModel;

        public bool Delete<T>(int id) where T : IBllModel;

        public IEnumerable<Video>? SearchVideos(int page = 0, int pageSize = 0, string? name = null, string? orderedBy = null);
    }
}
