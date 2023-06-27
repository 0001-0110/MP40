using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MP40.BLL.Mapping;
using MP40.BLL.Models.Authentication;
using MP40.BLL.Services;
using MP40.MVC.Mapping;
using MP40.MVC.Models;
using System.Security.Claims;

namespace MP40.MVC.Controllers
{
	public class LoginController : Controller
    {
        private readonly IBijectiveMapper<MvcMapperProfile> mapper;
        private readonly BLL.Services.IAuthenticationService authenticationService;
        private readonly IDataService dataService;

        public LoginController(IBijectiveMapper<MvcMapperProfile> mapper, BLL.Services.IAuthenticationService authenticationService, IDataService dataService)
        {
            this.mapper = mapper;
            this.authenticationService = authenticationService;
            this.dataService = dataService;
        }

        private async Task LogIn(bool persistent = false)
        {
            // From Task 12
            // TODO Do we give admin rights to everyone ?
            IEnumerable<Claim> claims = new Claim[] { new Claim(ClaimTypes.Name, authenticationService.User!.Username), new Claim(ClaimTypes.Role, "Admin") };
            ClaimsIdentity claimsIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties() { IsPersistent = persistent });
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Login()
        {
            // No need to login if already logged in
            // Mostly useful in case of `stay signed in`
            if (User.Identity?.IsAuthenticated ?? false)
                return RedirectToAction("Index", "Video");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginCredentials credentials)
        {
            if (!ModelState.IsValid)
            {
                credentials.Password = string.Empty;
                return View(credentials);
            }

            if (!authenticationService.TryAuthenticate(credentials))
            {
                ModelState.AddModelError(nameof(credentials.Password), "Invalid username or password");
                return View(credentials);
            }

            await LogIn(credentials.StaySignedIn);

            return RedirectToAction("Index", "Video");
        }

        public IActionResult Register()
        {
            ViewData["Countries"] = mapper.MapRange<Country>(dataService.GetAll<BLL.Models.Country>());

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterCredentials credentials)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Countries"] = mapper.MapRange<Country>(dataService.GetAll<BLL.Models.Country>());
                return View(credentials);
            }

            // Create user
            string errorKey;
            string errorMessage;
            if (!authenticationService.Register(credentials, out errorKey, out errorMessage))
            {
                // If an error occured, display it
                ModelState.AddModelError(errorKey, errorMessage);
                ViewData["Countries"] = mapper.MapRange<Country>(dataService.GetAll<BLL.Models.Country>());
                return View(credentials);
            }
            // Log in user
            await LogIn();

            // Redirect to home
            return RedirectToAction("Index", "Video");
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();

            return RedirectToAction(nameof(Login));
        }

        /*public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(VMChangePassword changePassword)
        {
            // TODO Change user password, skip BL for simplicity
            _userRepo.ChangePassword(
                changePassword.Username,
                changePassword.NewPassword);

            return RedirectToAction("Index");
            throw new NotImplementedException();
        }*/
    }
}
