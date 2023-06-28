using Microsoft.AspNetCore.Mvc;
using MP40.BLL.Mapping;
using MP40.BLL.Services;
using MP40.MVC.Mapping;
using MP40.MVC.Models;
using System.Runtime.InteropServices;

namespace MP40.MVC.Controllers.Administration
{
	public class VideoManagementController : AdminController<Video>
    {
        public VideoManagementController(IBijectiveMapper<MvcMapperProfile> mapper, IDataService dataService) : base(mapper, dataService) { }

        private void SendTagsAndGenres()
        {
			ViewData["Genres"] = mapper.MapRange<Genre>(dataService.GetAll<BLL.Models.Genre>());
			ViewData["Tags"] = mapper.MapRange<Tag>(dataService.GetAll<BLL.Models.Tag>());
		}

        public override IActionResult Create()
        {
            SendTagsAndGenres();
			return base.Create();
        }

        [HttpPost]
        public override IActionResult Create(Video video)
        {
            video.CreatedAt = DateTime.Now;
            return base.Create(video);
        }

        public override IActionResult Edit(int id)
        {
            SendTagsAndGenres();
            return base.Edit(id);
        }
    }
}
