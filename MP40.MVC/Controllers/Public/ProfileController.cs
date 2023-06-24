using Microsoft.AspNetCore.Mvc;
using MP40.BLL.Mapping;
using MP40.BLL.Services;
using MP40.MVC.Mapping;
using MP40.MVC.Models;

namespace MP40.MVC.Controllers.Public
{
	public class ProfileController : PublicController<User>
	{
		public ProfileController(IBijectiveMapper<MvcMapperProfile> mapper, IDataService dataService) : base(mapper, dataService) { }

		public override IActionResult Index()
		{
			if (!IsUserAuthorized())
				return RedirectToAction("Login", "Login");

			throw new NotImplementedException();
			/*User user = dataService.GetUser();
			return View(user);*/
		}
	}
}
