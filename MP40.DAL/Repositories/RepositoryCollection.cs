using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using MP40.DAL.DataBaseContext;
using MP40.DAL.Models;
using System.Reflection;

namespace MP40.DAL.Repositories
{
    public class RepositoryCollection : IRepositoryCollection
    {
        Dictionary<Type, object> repositories;

        public RepositoryCollection(RwaMoviesContext dbContext)
        {
            repositories = new Dictionary<Type, object>();
            // Get all models
            foreach (Type? modelType in Assembly.GetExecutingAssembly().GetTypes().Where(type => type.GetInterfaces().Contains(typeof(IDalModel))))
            {
                // Create the corresponding repository
                Type repositoryInstance = typeof(Repository<>).MakeGenericType(modelType);
                repositories.Add(modelType, Activator.CreateInstance(repositoryInstance, dbContext)!);
            }
        }

        public IRepository<T>? GetRepository<T>() where T : class, IDalModel
        {
            return repositories.GetValueOrDefault(typeof(T)) as IRepository<T>;
        }
    }
}
