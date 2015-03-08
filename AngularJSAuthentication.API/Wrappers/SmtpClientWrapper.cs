using System.Net.Mail;

namespace AngularJSAuthentication.API.Wrappers
{
    public class SmtpClientWrapper : ISmtpClientWrapper
    {
        private readonly SmtpClient _smtpClient;

        public SmtpClientWrapper()
        {
            _smtpClient = new SmtpClient();
        }

        public SmtpClient StmpClient()
        {
            return _smtpClient;
        }
    }
}