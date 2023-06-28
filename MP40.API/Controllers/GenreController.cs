using Microsoft.AspNetCore.Mvc;
using MP40.BLL.Models;
using MP40.BLL.Services;

namespace MP40.Controllers
{
	[Route("api/genres")]
	public class GenreController : ModelController<Genre>
    {
        public GenreController(ILogger<ModelController<Genre>> logger, IDataService dataService) : base(logger, dataService) { }
    }
}
