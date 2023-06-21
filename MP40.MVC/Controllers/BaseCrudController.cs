using Microsoft.AspNetCore.Mvc;
using MP40.BLL.Mapping;
using MP40.BLL.Services;
using MP40.MVC.Mapping;
using MP40.MVC.Models;

namespace MP40.MVC.Controllers
{
	public class BaseCrudController<TModel> : BaseController<TModel> where TModel : class, IViewModel
	{
		public BaseCrudController(IBijectiveMapper<MvcMapperProfile> mapper, IDataService dataService) : base(mapper, dataService) { }

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
		public IActionResult Delete(int id, Tag tag)
		{
			Type mappedType = mapper.GetMappedType(typeof(TModel));
			dataService.Delete(mappedType, id);
			return RedirectToAction(nameof(Index));
		}
	}
}
