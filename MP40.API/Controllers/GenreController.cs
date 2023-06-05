using MP40.BLL.Services;
using MP40.DAL.Models;

namespace MP40.Controllers
{
    public class GenreController : ModelController<Genre>
    {
        public GenreController(ILogger<ModelController<Genre>> logger, IDataService dataService) : base(logger, dataService) { }
    }
}
