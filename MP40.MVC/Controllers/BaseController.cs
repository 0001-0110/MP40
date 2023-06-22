using Microsoft.AspNetCore.Mvc;
using MP40.BLL.Mapping;
using MP40.BLL.Models;
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

		public IActionResult Details(int id)
		{
			Type mappedType = mapper.GetMappedType(typeof(TModel));
			return View(mapper.Map<TModel>(dataService.GetById(mappedType, id)));
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(TModel model)
		{
			Type mappedType = mapper.GetMappedType(typeof(TModel));
			var mappedModel = mapper.Map(mappedType, model);
			dataService.Create(mappedType, mappedModel!);
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Edit(int id)
		{
			Type mappedType = mapper.GetMappedType(typeof(TModel));
			return View(mapper.Map<TModel>(dataService.GetById(mappedType, id)));
		}

		[HttpPost]
		public IActionResult Edit(int id, TModel model)
		{
			Type mappedType = mapper.GetMappedType(typeof(TModel));
			object? mappedModel = mapper.Map(mappedType, model);
			dataService.Edit(mappedType, id, mappedModel!);
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Delete(int id)
		{
			Type mappedType = mapper.GetMappedType(typeof(TModel));
			return View(mapper.Map<TModel>(dataService.GetById(mappedType, id)));
		}

		[HttpPost]
		public IActionResult Delete(int id, TModel model)
		{
			Type mappedType = mapper.GetMappedType(typeof(TModel));
			dataService.Delete(mappedType, id);
			return RedirectToAction(nameof(Index));
		}
	}
}
