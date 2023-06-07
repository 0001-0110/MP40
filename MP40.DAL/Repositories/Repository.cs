using Microsoft.EntityFrameworkCore;
using MP40.DAL.DataBaseContext;
using MP40.DAL.Extensions;
using MP40.DAL.Models;
using System.Reflection;

namespace MP40.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IDalModel
    {
        protected RwaMoviesContext dbContext;

        // The property inside dbContext to fetch data from
        private readonly PropertyInfo property;

        public Repository(RwaMoviesContext rwaMoviesContext)
        {
            dbContext = rwaMoviesContext;
            property = dbContext.GetType().GetProperty(typeof(DbSet<T>)) ??
                throw new Exception("No corresponding property inside the dbContext");
        }

        private DbSet<T> Values => (DbSet<T>)property.GetValue(dbContext)!;

        public virtual IEnumerable<T> GetAll()
        {
            return Values;
        }

        public virtual IEnumerable<T> GetWhere(Predicate<T> predicate)
        {
            return Values.Where(item => predicate(item));
        }

        public virtual T? GetById(int id)
        {
            return Values.SingleOrDefault(video => video.Id == id);
        }

        public virtual void Create(T entity)
        {
            Values.Add(entity);
            dbContext.SaveChanges();
        }

        public virtual void Edit(int id, T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(T entity)
        {
            Values.Remove(entity);
            dbContext.SaveChanges();
        }
    }
}
