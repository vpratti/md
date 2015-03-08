using System.Net.Mail;

namespace AngularJSAuthentication.API.Wrappers
{
    public interface ISmtpClientWrapper
    {
        SmtpClient StmpClient();
    }
}