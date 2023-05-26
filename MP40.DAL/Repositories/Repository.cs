using MP40.DAL.DataBaseContext;

namespace MP40.DAL.Repositories
{
    public abstract class Repository<T> : IRepository<T>
    {
        protected RwaMoviesContext dbContext;

        public Repository(RwaMoviesContext rwaMoviesContext)
        {
            dbContext = rwaMoviesContext;
        }

        public abstract IEnumerable<T> GetAll();

        public abstract T? GetById(int id);

        public abstract void Create(T entity);

        public abstract void Edit(int id, T entity);

        public abstract void Delete(T entity);
    }
}
