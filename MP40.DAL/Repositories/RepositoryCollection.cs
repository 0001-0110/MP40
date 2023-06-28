using MP40.DAL.DataBaseContext;
using MP40.DAL.Models;
using System.Reflection;

namespace MP40.DAL.Repositories
{
	public class RepositoryCollection : IRepositoryCollection
	{
		Dictionary<Type, IRepository> repositories;

		public RepositoryCollection(RwaMoviesContext dbContext, IEnumerable<KeyValuePair<Type, Func<RwaMoviesContext, RepositoryCollection, IRepository>>>? factories = null)
		{
			repositories = new Dictionary<Type, IRepository>();

			// Get all models
			// First create all generics
			foreach (Type? modelType in Assembly.GetExecutingAssembly().GetTypes().Where(type => type.GetInterfaces().Contains(typeof(IDalModel))))
				repositories.Add(modelType, (IRepository)Activator.CreateInstance(typeof(Repository<>).MakeGenericType(modelType), dbContext)!);

			if (factories == null)
				return;

			// Then replace some by their specific implementation
			// We have to wait for all generics to be implemented since some repositories might need other repositories
			foreach (KeyValuePair<Type, Func<RwaMoviesContext, RepositoryCollection, IRepository>> factory in factories)
				repositories[factory.Key] = factory.Value.Invoke(dbContext, this);
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
