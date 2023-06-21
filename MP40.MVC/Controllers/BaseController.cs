using Microsoft.AspNetCore.Mvc;
using MP40.BLL.Mapping;
using MP40.BLL.Services;
using MP40.MVC.Mapping;
using MP40.MVC.Models;

namespace MP40.MVC.Controllers
{
    public class BaseController<TModel> : Controller where TModel : class, IViewModel
    {
        protected IBijectiveMapper<MvcMapperProfile> mapper;
        protected IDataService dataService;

        public BaseController(IBijectiveMapper<MvcMapperProfile> mapper, IDataService dataService)
        {
            this.mapper = mapper;
            this.dataService = dataService;
        }

		public IActionResult Index()
		{

			Type mappedType = mapper.GetMappedType(typeof(TModel));
			IEnumerable<object>? mappedObjects = dataService.GetAll(mappedType);
			IEnumerable<TModel> models = mapper.Map<IEnumerable<TModel>>(mappedObjects);
			return View(models);
		}
	}
}
