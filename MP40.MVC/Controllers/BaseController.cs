using Microsoft.AspNetCore.Mvc;
using MP40.BLL.Mapping;
using MP40.BLL.Services;
using MP40.MVC.Mapping;
using MP40.MVC.Models;
using MP40.BLL.Models;
using MP40.DAL.Extensions;

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

		private Page<TModel> GetPage(Page page)
		{
			Type mappedType = mapper.GetMappedType(typeof(TModel));
			return dataService.GetPageAndMap(mappedType, page, model => mapper.Map<TModel>(model))!;
		}

		public IActionResult Index()
		{
			Page<TModel> page = GetPage(new Page(1));

			return View(page);
		}

		public IActionResult IndexPartial(int pageIndex)
		{
			Page<TModel> page = GetPage(new Page(pageIndex));

			return PartialView(page);
		}
	}
}
