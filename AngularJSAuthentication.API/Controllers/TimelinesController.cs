using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AngularJSAuthentication.API.DbRepositories;
using AngularJSAuthentication.API.Models;
using AutoMapper;
using Seterlund.CodeGuard;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/Timelines")]
    public class TimelinesController : ApiController
    {
        private readonly ITimelineRepository _timelineRepository;
        private readonly IMappingEngine _mappingEngine;

        public TimelinesController() : this(new TimelineRepository(), Mapper.Engine) { }

        public TimelinesController(ITimelineRepository timelineRepository, IMappingEngine mappingEngine)
        {
            _timelineRepository = timelineRepository;
            _mappingEngine = mappingEngine;
        }

        [HttpPost]
        [Authorize]
        [Route("CreateTimeframe")]
        public async Task<IHttpActionResult> CreateTimeframe(NewTimeframeDto newTimeframeDto)
        {
            Guard.That(newTimeframeDto.Description).IsNotNullOrEmpty();
            Guard.That(newTimeframeDto.Name).IsNotNullOrEmpty();

            Timeframe result = _timelineRepository.CreateTimeframe(newTimeframeDto);

            TimeframeDto mappedResult = _mappingEngine.Map<Timeframe, TimeframeDto>(result);

            return Ok(mappedResult);
        }

        [HttpGet]
        [Authorize]
        [Route("GetTimeframes")]
        public async Task<IHttpActionResult> GetTimeframes()
        {
            List<Timeframe> result = _timelineRepository.GetTimeframes();

            List<TimeframeDto> mappedResult = _mappingEngine.Map<List<Timeframe>, List<TimeframeDto>>(result);

            return Ok(mappedResult);
        } 
    }
}