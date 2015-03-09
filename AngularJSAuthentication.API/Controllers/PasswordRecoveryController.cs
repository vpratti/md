using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http;
using AngularJSAuthentication.API.Extensions;
using AngularJSAuthentication.API.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Seterlund.CodeGuard;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/PasswordRecovery")]
    public class PasswordRecoveryController : ApiController
    {
        private readonly IEmailService _emailService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AuthRepository _authRepository;

        public PasswordRecoveryController() : this(new EmailService(), new UserManager<IdentityUser>(new UserStore<IdentityUser>(new AuthContext())), new AuthRepository()) { }

        public PasswordRecoveryController(IEmailService emailService, UserManager<IdentityUser> userManager, AuthRepository authRepository)
        {
            _emailService = emailService;
            _userManager = userManager;
            _authRepository = authRepository;
        }

        [HttpPut]
        [Route("ResetPassword")]
        public async Task<IHttpActionResult> ResetPassword(string email)
        {
            Guard.That(email).IsNotNullOrEmpty();

            IdentityUser user = _userManager.GetUserByEmail(email);

            var temporaryPassword = await _authRepository.ResetPassword(user);

            var mailMessage = new MailMessage("donotreply@virtualclarity.com", user.Email, "VirtualClarity-Passwor Reset", temporaryPassword);

            _emailService.SendEmail(user.Email, mailMessage);
            
            return Ok();
        } 
    }
}