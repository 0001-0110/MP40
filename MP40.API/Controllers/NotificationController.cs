using Microsoft.AspNetCore.Mvc;
using MP40.BLL.Services;
using MP40.DAL.Models;

namespace MP40.Controllers
{
    public class NotificationController : ModelController<Video>
    {
        public NotificationController(ILogger<ModelController<Video>> logger, IDataService dataService) : base(logger, dataService) { }

        [HttpPost("[action]")]
        public ActionResult SendAllNotifications()
        {
            throw new NotImplementedException();
        }
    }
}
