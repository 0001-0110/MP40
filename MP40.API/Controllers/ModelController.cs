using Microsoft.AspNetCore.Mvc;
using MP40.BLL.Models;
using MP40.BLL.Services;

namespace MP40.Controllers
{
    [ApiController]
    public abstract class ModelController<T> : ControllerBase where T : class, IBllModel
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
            model.Id = dataService.Create(model);
            if (model.Id == -1)
                return BadRequest();
            return Ok(model);
        }

        [HttpPut("{id}")]
        public ActionResult<T> Put(int id, [FromBody] T model)
        {
            if (!dataService.Edit(id, model))
                return NotFound();
            model.Id = id;
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
