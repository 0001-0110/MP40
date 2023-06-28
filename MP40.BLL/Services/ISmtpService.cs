using MP40.BLL.Models;

namespace MP40.BLL.Services
{
    public interface ISmtpService
    {
        bool Send(Notification notification);

        public bool SendUnsent();
    }
}
