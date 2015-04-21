using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AngularJSAuthentication.API.DbRepositories;
using AngularJSAuthentication.API.Models;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/ActivityTemplates")]
    public class ActivityTemplatesController : ApiController
    {
        private readonly IActivityTemplatesRepository _activityTemplatesRepository;

        public ActivityTemplatesController() : this(new ActivityTemplatesRepository()) { }

        public ActivityTemplatesController(IActivityTemplatesRepository activityTemplatesRepository)
        {
            _activityTemplatesRepository = activityTemplatesRepository;
        }

        [HttpGet]
        [Authorize]
        [Route("GetTemplates")]
        public async Task<IHttpActionResult> GetTemplates()
        {
            List<ActivityTemplate> result = _activityTemplatesRepository.GetTemplates();

            return Ok(result);
        }
    }
}