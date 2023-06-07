using MP40.BLL.Models;
using MP40.BLL.Services;

namespace MP40.Controllers
{
    public class TagController : ModelController<Tag>
    {
        public TagController(ILogger<ModelController<Tag>> logger, IDataService dataService) : base(logger, dataService) { }
    }
}
