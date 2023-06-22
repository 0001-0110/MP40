using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MP40.BLL.Models;
using MP40.BLL.Services;

namespace MP40.Controllers
{
	[Authorize]
	public class VideoController : ModelController<Video>
	{
		public VideoController(ILogger<ModelController<Video>> logger, IDataService dataService) : base(logger, dataService) { }

		[HttpGet("[action]")]
		public ActionResult<IEnumerable<Video>> Search(int page, int pageSize, string? filter = null, string? orderBy = null)
		{
			Func<Video, object> ordering = orderBy switch
			{
				"id" => video => video.Id,
				"name" => video => video.Name,
				"total_time" => video => video.TotalSeconds,
				_ => throw new NotImplementedException()
			};

			IEnumerable<Video>? models = dataService.GetPage<Video>(
				new Page(page, pageSize)
				{
					Filter = filter,
					FilterBy = "name",
					OrderBy = orderBy,
				}).Models;

			if (models == null)
				return BadRequest();

			return Ok(models);
		}
	}
}
