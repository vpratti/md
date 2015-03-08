using System.Net.Mail;

namespace AngularJSAuthentication.API.Services
{
    public interface IEmailService
    {
        void SendEmail(string email, MailMessage user);
    }
}