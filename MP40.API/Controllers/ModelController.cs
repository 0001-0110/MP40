using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MP40.BLL.Services;
using MP40.DAL.Models;

namespace MP40.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]s")]
    public abstract class ModelController<T> : ControllerBase where T : class, IModel
    {
        protected readonly ILogger<ModelController<T>> logger;
        protected readonly IDataService dataService;

        public ModelController(ILogger<ModelController<T>> logger, IDataService dataService)
        {
            this.logger = logger;
            this.dataService = dataService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<T>> Get()
        {
            return Ok(dataService.GetAll<T>());
        }

        [HttpGet("{id}")]
        public ActionResult<T> GetById(int id)
        {
            T? result = dataService.GetById<T>(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<T> Post([FromBody] T model)
        {
            if (!dataService.Create(model))
                return BadRequest();
            return Ok(model);
        }

        [HttpPut("{id}")]
        public ActionResult<T> Put(int id, [FromBody] T model)
        {
            if (!dataService.Edit(id, model))
                return NotFound();
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public ActionResult<T> Delete(int id)
        {
            if (!dataService.Delete<T>(id))
                return NotFound();
            return Ok();
        }
    }
}
