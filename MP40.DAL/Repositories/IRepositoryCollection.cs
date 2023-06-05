using MP40.DAL.Models;

namespace MP40.DAL.Repositories
{
    public interface IRepositoryCollection
    {
        object? GetRepository(Type type);

        IRepository<T>? GetRepository<T>() where T : class, IModel;
    }
}
