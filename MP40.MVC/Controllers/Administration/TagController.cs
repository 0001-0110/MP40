using Microsoft.AspNetCore.Mvc;
using MP40.BLL.Mapping;
using MP40.BLL.Services;
using MP40.MVC.Mapping;
using MP40.MVC.Models;

namespace MP40.MVC.Controllers.Administration
{
    public class TagController : BaseController
    {
        public TagController(IBijectiveMapper<MvcMapperProfile> mapper, IDataService dataService) : base(mapper, dataService) { }

        public IActionResult Index()
        {
            IEnumerable<BLL.Models.Tag> thing = dataService.GetAll<BLL.Models.Tag>();
            var workalready = mapper.Map<IEnumerable<Tag>>(thing);
            return View(workalready);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Tag tag)
        {
            var mappdanstatete = mapper.Map<BLL.Models.Tag>(tag);
            dataService.Create(mappdanstatete);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id) 
        {
            return View(mapper.Map<Tag>(dataService.GetById<BLL.Models.Tag>(id)));
        }

        [HttpPost]
        public IActionResult Edit(int id, Tag tag)
        {
			var mappdanstatete = mapper.Map<BLL.Models.Tag>(tag);
			dataService.Edit(id, mappdanstatete);
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Delete(int id)
		{
			return View(mapper.Map<Tag>(dataService.GetById<BLL.Models.Tag>(id)));
		}

		[HttpPost]
		public IActionResult Delete(int id, Tag tag)
		{
			var mappdanstatete = mapper.Map<BLL.Models.Tag>(tag);
			dataService.Delete<BLL.Models.Tag>(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
