using Microsoft.AspNetCore.Mvc;
using MP40.BLL.Models;
using MP40.BLL.Services;

namespace MP40.Controllers
{
    public class NotificationController : ModelController<Notification>
    {
        private readonly ISmtpService smtpService;

        public NotificationController(ILogger<ModelController<Notification>> logger, ISmtpService smtpService, IDataService dataService) : base(logger, dataService) 
        {
            this.smtpService = smtpService;
        }

        [HttpGet("[action]")]
        public ActionResult<int> GetUnsentNotificationCount()
        {
            return Ok(dataService.GetAll<Notification>().Where(notification => notification.SentAt == null).Count());
        }

        [HttpPost("[action]")]
        public ActionResult SendAllNotifications()
        {
            if (!smtpService.SendUnsent())
                return StatusCode(500);
            return Ok();
        }
    }
}
