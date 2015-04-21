using System.Collections.Generic;
using AngularJSAuthentication.API.Dto;
using AngularJSAuthentication.API.Models;

namespace AngularJSAuthentication.API.DbRepositories
{
    public interface IActivityTemplatesRepository
    {
        List<ActivityTemplate> GetTemplates();

        ActivityTemplate CreateActivityTemplate(NewActivityTemplateDto newActivityTemplateDto);

        ActivityTask CreateActivityTask(string name);

        TemplateTask CreateTemplateTask(NewTemplateTaskDto newTemplateTaskDto);

        List<ActivityTask> GetActivityTasks();
    }
}