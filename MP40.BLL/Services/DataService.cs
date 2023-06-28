using MP40.BLL.Mapping;
using MP40.BLL.Models;
using MP40.DAL.Extensions;
using MP40.DAL.Repositories;

namespace MP40.BLL.Services
{
	public class DataService : IDataService
	{
		private readonly IBijectiveMapper<BllMapperProfile> mapper;
		private readonly IRepositoryCollection repositoryCollection;

		public DataService(IBijectiveMapper<BllMapperProfile> mapper, IRepositoryCollection repositoryCollection)
		{
			this.mapper = mapper;
			this.repositoryCollection = repositoryCollection;
		}

		#region Generics

		private void InvokeRepository<TModel>(string methodName, params object[] arguments) where TModel : class, IBllModel
		{
			InvokeRepository<TModel, object>(methodName, arguments);
		}

		private TResult? InvokeRepository<TModel, TResult>(string methodName, params object[] arguments) where TModel : class, IBllModel
		{
			Type modelType = mapper.GetMappedType(typeof(TModel))!;
			object? repository = repositoryCollection.GetRepository(modelType);
			return (TResult?)repository?.CallMethod(methodName, arguments);
		}

		public IEnumerable<object>? GetAll(Type type)
		{
			return (IEnumerable<object>?)this.CallGeneric(nameof(GetAll), type);
		}

		public IEnumerable<T> GetAll<T>() where T : class, IBllModel
		{
			IEnumerable<object>? result = InvokeRepository<T, IEnumerable<object>>("GetAll")!;
			return mapper.MapRange<T>(result);
		}

		public Page<TMapped>? GetPageAndMap<TMapped>(Type type, object page, Func<object, TMapped> mapping) where TMapped : class
		{
			return (Page<TMapped>?)this.CallGeneric(nameof(GetPageAndMap), new Type[] { typeof(TMapped), type, }, page, mapping);
		}

		public Page<TMapped> GetPageAndMap<TMapped, T>(Page page, Func<object, TMapped> mapping) where TMapped : class where T : class, IBllModel
		{
			return GetPage<T>(page).Map(mapping);
		}

		public object? GetPage(Type type, object page)
		{
			return this.CallGeneric(nameof(GetPage), type, page);
		}

		/// <summary>
		/// Fills the page according to the selected page, page size, the filtering and the ordering
		/// </summary>
		/// <typeparam name="T">Type of the models</typeparam>
		/// <param name="page"></param>
		/// <returns>Teh same page it was given, but with models inside</returns>
		public Page<T> GetPage<T>(Page page) where T : class, IBllModel
		{
			IEnumerable<T> models = GetAll<T>();

			// Filter first, so that the number of page is correctly computed
			if (page.Filter != null)
			{
				Func<T, string?> filterBy = page.FilterBy switch
				{
					"name" => model => (model as INamedModel)?.Name,
					"genre" => model => (model as IGenredModel)?.Genre?.Name,
					"firstname" => model => (model as IUser)?.FirstName,
					"lastname" => model => (model as IUser)?.LastName,
					"country" => model => 
					{ 
						int countryId = (model as IUser)?.CountryId ?? -1;
						return countryId == -1 ? null : GetById<Country>(countryId)?.Name;
					},
					_ => throw new ArgumentException()
				};
				// If the cast fails, return false for every object
				models = models.Where(model => filterBy(model)?.Contains(page.Filter, StringComparison.OrdinalIgnoreCase) ?? false);
			}

			// Even tho the number of pages is the correct one here, the partial views do not update the button count
			// This is a bug coming from the views, not c#
			// Compute total number of pages
			page.PageCount = (int)Math.Ceiling(models.Count() / (decimal)page.PageSize);

			// This should never happen, but you never know
			if (page.PageIndex > 0 && page.PageSize > 0)
				// The first page is page 1
				models = models.Skip((page.PageIndex - 1) * page.PageSize).Take(page.PageSize);

			if (page.OrderBy != null)
				models = models.OrderBy<T, object?>(
					page.OrderBy switch
					{
						"name" => model => (model as INamedModel)?.Name,
						_ => throw new ArgumentException()
					});

			return new Page<T>(page, models);
		}

		public IEnumerable<object>? GetWhere(Type type, object predicate)
		{
			return (IEnumerable<object>?)this.CallGeneric(nameof(GetWhere), type, predicate);
		}

		public IEnumerable<T> GetWhere<T>(Predicate<T> predicate) where T : class, IBllModel
		{
			IEnumerable<object>? result = InvokeRepository<T, IEnumerable<object>>("GetWhere", predicate)!;
			return mapper.MapRange<T>(result);
		}

		public object? GetById(Type type, int id)
		{
			return this.CallGeneric(nameof(GetById), type, id);
		}

		public T? GetById<T>(int id) where T : class, IBllModel
		{
			object? result = InvokeRepository<T, object>("GetById", id);
			return mapper.Map<T>(result);
		}

		public int Create(Type type, object model)
		{
			return (int)this.CallGeneric(nameof(Create), type, model)!;
		}

		public int Create<T>(T model) where T : class, IBllModel
		{
			Type mappedType = mapper.GetMappedType(typeof(T));
			return InvokeRepository<T, int>("Create", mapper.Map(mappedType, model)!);
		}

		public bool Edit(Type type, int id, object model)
		{
			return (bool)this.CallGeneric(nameof(Edit), type, id, model)!;
		}

		public bool Edit<T>(int id, T model) where T : class, IBllModel
		{
			Type mappedType = mapper.GetMappedType(typeof(T));
			return InvokeRepository<T, bool>("Edit", id, mapper.Map(mappedType, model)!);
		}

		public bool Delete(Type type, int id)
		{
			return (bool)this.CallGeneric(nameof(Delete), type, id)!;
		}

		public bool Delete<T>(int id) where T : class, IBllModel
		{
			return InvokeRepository<T, bool>("Delete", id);
		}

		#endregion

		// Soft delete
		public bool DeleteUser(int id)
		{
			User? user = GetById<User>(id);

			if (user == null)
				return false;

			user.DeletedAt = DateTime.Now;
			Edit(user.Id, user);
			return true;
		}
	}
}
