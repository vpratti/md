using System.Net.Mail;

namespace AngularJSAuthentication.API
{
    public interface ISmtpClientWrapper
    {
        SmtpClient StmpClient();
    }
}