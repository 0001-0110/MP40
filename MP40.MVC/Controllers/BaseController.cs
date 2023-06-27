using Microsoft.AspNetCore.Mvc;
using MP40.BLL.Mapping;
using MP40.BLL.Models;
using MP40.BLL.Services;
using MP40.MVC.Mapping;
using MP40.MVC.Models;

namespace MP40.MVC.Controllers
{
	public abstract class BaseController<TModel> : Controller where TModel : class, IViewModel
	{
		protected IBijectiveMapper<MvcMapperProfile> mapper;
		protected IDataService dataService;

		public BaseController(IBijectiveMapper<MvcMapperProfile> mapper, IDataService dataService)
		{
			this.mapper = mapper;
			this.dataService = dataService;
		}

		protected abstract bool IsUserAuthorized();

		private Page<TModel> GetPage(Page page)
		{
			Type mappedType = mapper.GetMappedType(typeof(TModel));
			return dataService.GetPageAndMap(mappedType, page, model => mapper.Map<TModel>(model))!;
		}

		public virtual IActionResult Index()
		{
			if (!IsUserAuthorized())
				return RedirectToAction("Login", "Login");

			Page<TModel> page = GetPage(new Page(1));

			return View(page);
		}

		public virtual IActionResult IndexPartial(int pageIndex, string filterBy, string filter)
		{
            if (!IsUserAuthorized())
                return RedirectToAction("Login", "Login");

            Page<TModel> page = GetPage(new Page(pageIndex) 
			{
				FilterBy = filterBy,
				Filter = filter,
			});

			return PartialView(page);
		}

        public virtual IActionResult Details(int id)
		{
            if (!IsUserAuthorized())
                return RedirectToAction("Login", "Login");

            Type mappedType = mapper.GetMappedType(typeof(TModel));
			return View(mapper.Map<TModel>(dataService.GetById(mappedType, id)));
		}

		public virtual IActionResult Create()
		{
            if (!IsUserAuthorized())
                return RedirectToAction("Login", "Login");

            return View();
		}

		[HttpPost]
		public virtual IActionResult Create(TModel model)
		{
            if (!IsUserAuthorized())
                return RedirectToAction("Login", "Login");

            Type mappedType = mapper.GetMappedType(typeof(TModel));
			var mappedModel = mapper.Map(mappedType, model);
			dataService.Create(mappedType, mappedModel!);
			return RedirectToAction(nameof(Index));
		}

		public virtual IActionResult Edit(int id)
		{
            if (!IsUserAuthorized())
                return RedirectToAction("Login", "Login");

            Type mappedType = mapper.GetMappedType(typeof(TModel));
			return View(mapper.Map<TModel>(dataService.GetById(mappedType, id)));
		}

		[HttpPost]
		public virtual IActionResult Edit(int id, TModel model)
		{
            if (!IsUserAuthorized())
                return RedirectToAction("Login", "Login");

            Type mappedType = mapper.GetMappedType(typeof(TModel));
			object? mappedModel = mapper.Map(mappedType, model);
			dataService.Edit(mappedType, id, mappedModel!);
			return RedirectToAction(nameof(Index));
		}

		public virtual IActionResult Delete(int id)
		{
            if (!IsUserAuthorized())
                return RedirectToAction("Login", "Login");

            Type mappedType = mapper.GetMappedType(typeof(TModel));
			return View(mapper.Map<TModel>(dataService.GetById(mappedType, id)));
		}

		[HttpPost]
		public virtual IActionResult Delete(int id, TModel model)
		{
            if (!IsUserAuthorized())
                return RedirectToAction("Login", "Login");

            Type mappedType = mapper.GetMappedType(typeof(TModel));
			dataService.Delete(mappedType, id);
			return RedirectToAction(nameof(Index));
		}
	}
}
