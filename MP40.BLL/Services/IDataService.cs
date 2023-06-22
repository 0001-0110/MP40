using MP40.BLL.Models;
using MP40.BLL.Models.Authentication;

namespace MP40.BLL.Services
{
	public interface IDataService
	{
		IEnumerable<object>? GetAll(Type type);

		IEnumerable<T> GetAll<T>() where T : class, IBllModel;

		Page<TMapped>? GetPageAndMap<TMapped>(Type type, object page, Func<object, TMapped> mapping) where TMapped : class;

		Page<TMapped> GetPageAndMap<TMapped, T>(Page page, Func<object, TMapped> mapping) where TMapped : class where T : class, IBllModel;

		object? GetPage(Type type, object page);

		Page<T> GetPage<T>(Page page) where T : class, IBllModel;

		IEnumerable<object>? GetWhere(Type type, object predicate);

		IEnumerable<T> GetWhere<T>(Predicate<T> predicate) where T : class, IBllModel;

		object? GetById(Type type, int id);

		T? GetById<T>(int id) where T : class, IBllModel;

		bool Create(Type type, object model);

		bool Create<T>(T model) where T : class, IBllModel;

		bool Edit(Type type, int id, object model);

		bool Edit<T>(int id, T model) where T : class, IBllModel;

		bool Delete(Type type, int id);

		bool Delete<T>(int id) where T : class, IBllModel;

		User? GetUser(Credentials credentials);
	}
}
