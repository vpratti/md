using System.Net;
using System.Net.Http;
using System.Web.Http;
using Seterlund.CodeGuard;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/PasswordRecovery")]
    public class PasswordRecoveryController : ApiController
    {
        private readonly IEmailService _emailService;

        public PasswordRecoveryController() : this(new EmailService()) { }

        public PasswordRecoveryController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPut]
        [Route("ResetPassword")]
        public HttpResponseMessage ResetPassword(string email)
        {
            Guard.That(email).IsNotNullOrEmpty();
            _emailService.SendEmail();

            return Request.CreateResponse(HttpStatusCode.OK);
        } 
    }
}