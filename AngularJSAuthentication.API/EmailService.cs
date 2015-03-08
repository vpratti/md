using System.Net;
using System.Net.Mail;
using System.Text;

namespace AngularJSAuthentication.API
{
    public class EmailService : IEmailService
    {
        private readonly ISmtpClientWrapper _smtpClientWrapper;

        public EmailService():this(new SmtpClientWrapper()){}

        public EmailService(ISmtpClientWrapper smtpClientWrapper)
        {
            _smtpClientWrapper = smtpClientWrapper;
        }

        public void SendEmail()
        {
            SmtpClient client = _smtpClientWrapper.StmpClient();
            client.Port = 587; //todo make configurable
            client.Host = "smtp.gmail.com"; //todo make configurable
            client.EnableSsl = true;
            client.Timeout = 20000; //todo make configurable
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential("smvctestacct@gmail.com", "Vclarity@");//todo make configurable
            
            var mailMessage = new MailMessage("donotreply@virtualclarity.com", "superson727@gmail.com", "test", "test");
            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        
            client.Send(mailMessage);
        }
    }
}