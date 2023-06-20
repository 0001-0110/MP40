using Microsoft.AspNetCore.Mvc;

namespace MP40.MVC.Controllers.Administration
{
    public class VideoManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
