using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AngularJSAuthentication.API.DbRepositories;
using AngularJSAuthentication.API.Dto;
using AngularJSAuthentication.API.Models;
using AutoMapper;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/ActivityTemplates")]
    public class ActivityTemplatesController : ApiController
    {
        private readonly IActivityTemplatesRepository _activityTemplatesRepository;
        private readonly IMappingEngine _mapppingEngine;

        public ActivityTemplatesController() : this(new ActivityTemplatesRepository(), Mapper.Engine) { }

        public ActivityTemplatesController(IActivityTemplatesRepository activityTemplatesRepository, IMappingEngine mappingEngine)
        {
            _activityTemplatesRepository = activityTemplatesRepository;
            _mapppingEngine = mappingEngine;
        }

        [HttpGet]
        [Authorize]
        [Route("GetTemplates")]
        public async Task<IHttpActionResult> GetTemplates()
        {
            List<ActivityTemplate> result = _activityTemplatesRepository.GetTemplates();

            List<ActivityTemplateDto> mappedResult =
                _mapppingEngine.Map<List<ActivityTemplate>, List<ActivityTemplateDto>>(result);

            return Ok(mappedResult);
        }

        [HttpGet]
        [Authorize]
        [Route("GetActivityTasks")]
        public async Task<IHttpActionResult> GetActivityTasks()
        {
            List<ActivityTask> result = _activityTemplatesRepository.GetActivityTasks();

            List<ActivityTaskDto> mappedResult = _mapppingEngine.Map<List<ActivityTask>, List<ActivityTaskDto>>(result);

            return Ok(mappedResult);
        }

        [HttpPost]
        [Authorize]
        [Route("CreateTemplate")]
        public async Task<IHttpActionResult> CreateTemplate(NewActivityTemplateDto newActivityTemplateDto)
        {
            ActivityTemplate result =
                _activityTemplatesRepository.CreateActivityTemplate(newActivityTemplateDto);

            var mappedResult = _mapppingEngine.Map<ActivityTemplate, ActivityTemplateDto>(result);

            return Ok(mappedResult);
        }

        [HttpPut]
        [Authorize]
        [Route("UpdateTemplate")]
        public async Task<IHttpActionResult> UpdateTemplate(ActivityTemplateDto activityTemplateDto)
        {
            ActivityTemplate result = await _activityTemplatesRepository.UpdateTemplate(activityTemplateDto);

            var mappedResult = _mapppingEngine.Map<ActivityTemplate, ActivityTemplateDto>(result);

            return Ok(mappedResult);
        }

        [HttpPost]
        [Authorize]
        [Route("CreateTemplateTask")]
        public async Task<IHttpActionResult> CreateTemplateTask(NewTemplateTaskDto newTemplateTaskDto)
        {
            if (newTemplateTaskDto.TaskId == null)
            {
                ActivityTask activityTask =
                    _activityTemplatesRepository.CreateActivityTask(newTemplateTaskDto.ActivityTaskName);

                newTemplateTaskDto.TaskId = activityTask.Id;
            }

            TemplateTask result = _activityTemplatesRepository.CreateTemplateTask(newTemplateTaskDto);

            TemplateTaskDto mappedResult = _mapppingEngine.Map<TemplateTask, TemplateTaskDto>(result);
 
            return Ok(mappedResult);
        }

        [HttpPut]
        [Authorize]
        [Route("UpdateTemplateTask")]
        public async Task<IHttpActionResult> UpdateTemplateTask(TemplateTaskDto templateTaskDto)
        {
            TemplateTask result = await _activityTemplatesRepository.UpdateTemplateTask(templateTaskDto);

            TemplateTaskDto mappedResult = _mapppingEngine.Map<TemplateTask, TemplateTaskDto>(result);

            return Ok(mappedResult);
        }

        [HttpDelete]
        [Authorize]
        [Route("DeleteTemplate")]
        public async Task<IHttpActionResult> DeleteTemplate(long id)
        {
            await _activityTemplatesRepository.DeleteTemplate(id);

            return Ok();
        }

        [HttpDelete]
        [Authorize]
        [Route("DeleteTemplateTask")]
        public async Task<IHttpActionResult> DeleteTemplateTask([FromUri] DeleteTemplateTaskObj deleteTemplateTaskObj)
        {
            await _activityTemplatesRepository.DeleteTemplateTask(deleteTemplateTaskObj);

            return Ok();
        }
    }
}