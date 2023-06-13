using MP40.DAL.DataBaseContext;
using MP40.DAL.Models;
using System.Reflection;

namespace MP40.DAL.Repositories
{
    public class RepositoryCollection : IRepositoryCollection
    {
        Dictionary<Type, IRepository> repositories;

        public RepositoryCollection(RwaMoviesContext dbContext)
        {
            repositories = new Dictionary<Type, IRepository>();

            // Get all models
            foreach (Type? modelType in Assembly.GetExecutingAssembly().GetTypes().Where(type => type.GetInterfaces().Contains(typeof(IDalModel))))
            {
                // Create the corresponding repository
                Type repositoryInstance = typeof(Repository<>).MakeGenericType(modelType);
                repositories.Add(modelType, (IRepository)Activator.CreateInstance(repositoryInstance, dbContext)!);
            }
        }

        public object? GetRepository(Type modelType)
        {
            return repositories.GetValueOrDefault(modelType);
        }

        public IRepository<T>? GetRepository<T>() where T : class, IDalModel
        {
            return GetRepository(typeof(T)) as IRepository<T>;
        }
    }
}
