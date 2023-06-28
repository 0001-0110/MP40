using Microsoft.AspNetCore.Mvc;
using MP40.BLL.Mapping;
using MP40.BLL.Models.Authentication;
using MP40.BLL.Services;
using MP40.MVC.Mapping;
using MP40.MVC.Models;

namespace MP40.MVC.Controllers.Administration
{
	public class UserController : AdminController<User>
	{
		private readonly IAuthenticationService authenticationService;

		public UserController(IBijectiveMapper<MvcMapperProfile> mapper, IDataService dataService, IAuthenticationService authenticationService) : base(mapper, dataService)
		{
			this.authenticationService = authenticationService;
		}

		private void SendCountries()
		{
			ViewData["Countries"] = mapper.MapRange<Country>(dataService.GetAll<BLL.Models.Country>());
		}

		public override IActionResult Create()
		{
			SendCountries();
			return base.Create();
		}

		public IActionResult CreateUser(RegisterCredentials credentials)
		{
			if (!IsUserAuthorized())
				return RedirectToAction("Login", "Login");

            if (!ModelState.IsValid)
            {
				SendCountries();
                return View("Create", credentials);
            }

			// Create user
			if (!authenticationService.Register(credentials, out string errorKey, out string errorMessage))
			{
				// If an error occured, display it
				ModelState.AddModelError(errorKey, errorMessage);
				SendCountries();
				return View("Create", credentials);
			}
			// Do not log in the user

			// Redirect to user list
			return RedirectToAction(nameof(Index));
        }

		public override IActionResult Edit(int id)
		{
			SendCountries();
			return base.Edit(id);
		}

		[HttpPost]
		public override IActionResult Delete(int id, User model)
		{
			if (!IsUserAuthorized())
				return RedirectToAction("Login", "Login");

			dataService.DeleteUser(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
