using Microsoft.AspNetCore.Mvc;
using MP40.DAL.Repositories;
using MP40.DAL.Models;

namespace MP40.Controllers
{
    [ApiController]
    [Route("api/videos")]
    public class VideoController : ControllerBase
    {
        private readonly ILogger<VideoController> logger;
        private readonly IRepository<Video> videoRepository;

        public VideoController(ILogger<VideoController> logger, IRepository<Video> videoRepository)
        {
            this.logger = logger;
            this.videoRepository = videoRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Video>> Get()
        {
            return Ok(videoRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Video> Get(int id)
        {
            var result = videoRepository.GetById(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<Video> Post([FromBody] Video video)
        {
            videoRepository.Create(video);
            return Ok(video);
        }

        [HttpPut]
        public ActionResult<Video> Put(int id, [FromBody] Video video)
        {
            if (videoRepository.GetById(id) == null)
                return NotFound();

            videoRepository.Edit(id, video);
            return Ok(video);
        }

        [HttpDelete]
        public ActionResult<Video> Delete(int id) 
        {
            Video? video = videoRepository.GetById(id);
            
            if (video == null)
                return NotFound();

            videoRepository.Delete(video);
            return Ok(video);
        }
    }
}
