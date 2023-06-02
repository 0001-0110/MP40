using MP40.BLL.Models;
using MP40.BLL.Services;

namespace MP40.Controllers
{
    public class GenreController : Controller<Genre>
    {
        public GenreController(ILogger<Controller<Genre>> logger, IDataService dataService) : base(logger, dataService) { }
    }
}
