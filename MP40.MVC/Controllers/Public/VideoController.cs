using Microsoft.AspNetCore.Mvc;

namespace MP40.MVC.Controllers.Public
{
    public class VideoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
