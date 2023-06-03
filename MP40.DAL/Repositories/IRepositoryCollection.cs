using MP40.DAL.Models;

namespace MP40.DAL.Repositories
{
    public interface IRepositoryCollection
    {
        IRepository<T>? GetRepository<T>() where T : class, IDalModel;
    }
}
