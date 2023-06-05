using MP40.BLL.Services;
using MP40.DAL.Models;

namespace MP40.Controllers
{
    public class TagController : ModelController<Tag>
    {
        public TagController(ILogger<ModelController<Tag>> logger, IDataService dataService) : base(logger, dataService) { }
    }
}
