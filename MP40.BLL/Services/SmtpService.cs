using MP40.BLL.Models;
using System.Net.Mail;

namespace MP40.BLL.Services
{
    public class SmtpService : ISmtpService
    {
        private const string SENDERADDRESS = "noreply@mp40.com";

        private readonly IDataService dataService;

        public SmtpService(IDataService dataService) 
        {
            this.dataService = dataService;
        }

        public bool Send(Notification notification)
        {
            SmtpClient smtpClient = new("localhost");
            MailMessage message = new(new MailAddress(SENDERADDRESS), new MailAddress(notification.ReceiverEmail))
            {
                Subject = notification.Subject,
                Body = notification.Body,
            };

            try { smtpClient.Send(message); }
            catch { return false; }

            notification.SentAt = DateTime.Now;
            dataService.Edit(notification.Id, notification);
            return true;
        }

        public bool SendUnsent()
        {
            bool success = true;
            foreach (Notification notification in dataService.GetAll<Notification>().Where(notification => notification.SentAt == null))
                success &= Send(notification);
            return success;
        }
    }
}
