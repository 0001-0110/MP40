using Microsoft.AspNetCore.Mvc;
using MP40.BLL.Models;
using MP40.BLL.Services;

namespace MP40.Controllers
{
    [Route("api/videos")]
    public class VideoController : Controller<Video>
    {
        public VideoController(ILogger<Controller<Video>> logger, IDataService dataService) : base(logger, dataService) { }

        [HttpGet("[action]")]
        public ActionResult<IEnumerable<Video>> Search(int page = 0, int pageSize = 0, string? name = null, string? orderedBy = null)
        {
            var result = dataService.SearchVideos(page, pageSize, name, orderedBy);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }
    }
}
