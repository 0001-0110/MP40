using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MP40.BLL.Models;
using MP40.BLL.Models.Authentication;
using MP40.BLL.Services;
using MP40.MVC.Models;
using System.Security.Claims;

namespace MP40.MVC.Controllers
{
    public class LoginController : Controller
    {
        private IDataService dataService;

        public LoginController(IDataService dataService)
        {
            this.dataService = dataService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(VMLogin login)
        {
            if (!ModelState.IsValid)
                return View(login);

            // Might want to add mapping
            BLL.Models.User? user = dataService.GetUser(new Credentials
            {
               Username = login.Username,
               Password = login.Password
            });

            if (user == null)
            {
                ModelState.AddModelError("Username", "Invalid username or password");
                return View(login);
            }

            // TODO What is this monstrosity ?
            List<Claim> claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties()).Wait();

            return RedirectToAction("Index", "Video");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(VMRegister register)
        {
            if (!ModelState.IsValid)
                return View(register);

            // TODO Create user from register
            
            // Redirect to home
            return RedirectToAction("Index", "Video");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();

            return RedirectToAction("Login");
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(VMChangePassword changePassword)
        {
            // TODO Change user password, skip BL for simplicity
            /*_userRepo.ChangePassword(
                changePassword.Username,
                changePassword.NewPassword);

            return RedirectToAction("Index");*/
            throw new NotImplementedException();
        }
    }
}
