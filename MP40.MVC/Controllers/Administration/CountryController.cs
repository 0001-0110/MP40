using Microsoft.AspNetCore.Mvc;
using MP40.BLL.Mapping;
using MP40.BLL.Services;
using MP40.MVC.Mapping;
using MP40.MVC.Models;

namespace MP40.MVC.Controllers.Administration
{
    public class CountryController : BaseController
    {
        public CountryController(IBijectiveMapper<MvcMapperProfile> mapper, IDataService dataService) : base(mapper, dataService)
        {
            this.mapper = mapper;
            this.dataService = dataService;
        }

        public IActionResult Index()
        {
            IEnumerable<BLL.Models.Country> thing = dataService.GetAll<BLL.Models.Country>();
            var workalready = mapper.Map<IEnumerable<Country>>(thing);
            return View(workalready);
        }
    }
}
