using System.Net;
using System.Net.Mail;
using System.Text;
using AngularJSAuthentication.API.Wrappers;

namespace AngularJSAuthentication.API.Services
{
    public class EmailService : IEmailService
    {
        private readonly ISmtpClientWrapper _smtpClientWrapper;

        public EmailService():this(new SmtpClientWrapper()){}

        public EmailService(ISmtpClientWrapper smtpClientWrapper)
        {
            _smtpClientWrapper = smtpClientWrapper;
        }

        public void SendEmail(string email, MailMessage message)
        {
            SmtpClient client = _smtpClientWrapper.StmpClient();
            client.Port = 587; //todo make configurable in webconfig
            client.Host = "smtp.gmail.com"; //todo make configurable in webconfig
            client.EnableSsl = true;
            client.Timeout = 20000; //todo make configurable in webconfig
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential("myacct@gmail.com", "password");//todo make configurable in webconfig

            var mailMessage = message;
            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        
            client.Send(mailMessage);
        }
    }
}