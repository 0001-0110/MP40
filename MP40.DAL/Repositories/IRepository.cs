using MP40.DAL.Models;

namespace MP40.DAL.Repositories
{
    public interface IRepository
    {

    }

    public interface IRepository<T> : IRepository where T : class, IDalModel
    {
        IEnumerable<T> GetAll();

        IEnumerable<T> GetWhere(Predicate<T> predicate);

        T? GetById(int id);

        bool Create(T entity);

        bool Edit(int id, T entity);

        bool Delete(T entity);
    }
}
