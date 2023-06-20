using Microsoft.AspNetCore.Mvc;
using MP40.BLL.Mapping;
using MP40.BLL.Services;
using MP40.MVC.Mapping;

namespace MP40.MVC.Controllers
{
    // Unused for now
    public class BaseController : Controller
    {
        protected IBijectiveMapper<MvcMapperProfile> mapper;
        protected IDataService dataService;

        public BaseController(IBijectiveMapper<MvcMapperProfile> mapper, IDataService dataService)
        {
            this.mapper = mapper;
            this.dataService = dataService;
        }

        /*public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/
    }
}
