using System.Threading.Tasks;
using System.Web.Http;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/Lookup")]
    public class LookupsController : ApiController
    {
        private readonly LookupContext _context;

        public LookupsController()
        {
            _context = new LookupContext();
        }

        [HttpGet]
        [Authorize]
        [Route("GetCategories")]
        public async Task<IHttpActionResult> GetCategories()
        {
            var categories = _context.Categories;
            return Ok(categories);
        }

        [HttpPost]
        [Authorize]
        [Route("CreateCategory")]
        public async Task<IHttpActionResult> CreateCategory()
        {
            return Ok();
        } 
    }
}