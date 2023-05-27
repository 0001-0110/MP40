using MP40.DAL.Models;

namespace MP40.DAL.Repositories
{
    public interface IRepository<T> where T : class, IModel
    {
        IEnumerable<T> GetAll();
        
        T? GetById(int id);

        void Create(T entity);

        void Edit(int id, T entity);

        void Delete(T entity);
    }
}
