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

        public virtual int Create(T entity)
        {
            try
            {
                Values.Add(entity);
                dbContext.SaveChanges();
            }
            catch { return -1; }
            return entity.Id;
        }

        public virtual bool Edit(int id, T edit)
        {
            try
            {
                T? entity = GetById(id);
                if (entity == null)
                    return false;
                entity.CopyDataFrom(edit, true);
                dbContext.Update(entity);
                dbContext.SaveChanges();
            }
            catch { return false; }
            return true;
        }

        public virtual bool Delete(int id)
        {
            return Delete(GetById(id));
        }

        public virtual bool Delete(T? entity)
        {
            if (entity == null)
                return false;

            try
            {
                Values.Remove(entity);
                dbContext.SaveChanges();
            }
            catch { return false; }
            return true;
        }
    }
}
