using MP40.BLL.Models;
using MP40.BLL.Services;

namespace MP40.Controllers
{
    public class UserController : ModelController<User>
    {
        public UserController(ILogger<ModelController<User>> logger, IDataService dataService) : base(logger, dataService) { }
    }
}
