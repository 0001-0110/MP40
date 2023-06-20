using Microsoft.AspNetCore.Mvc;

namespace MP40.MVC.Controllers.Administration
{
    public class GenreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
