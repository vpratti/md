using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AngularJSAuthentication.API.DbContexts;
using AngularJSAuthentication.API.Dto;
using AngularJSAuthentication.API.Models;

namespace AngularJSAuthentication.API.DbRepositories
{
    public class ActivityTemplatesRepository : IActivityTemplatesRepository, IDisposable
    {
        private readonly LookupContext _context;

        public ActivityTemplatesRepository()
        {
            _context = new LookupContext();
        }

        public List<ActivityTemplate> GetTemplates()
        {
            return _context.ActivityTemplates.ToList();
        }

        public ActivityTemplate CreateActivityTemplate(NewActivityTemplateDto newActivityTemplateDto)
        {
            var activityTemplate = new ActivityTemplate(newActivityTemplateDto, HttpContext.Current.User.Identity.Name);

            _context.ActivityTemplates.Add(activityTemplate);

            _context.SaveChanges();

            return activityTemplate;
        }

        public ActivityTask CreateActivityTask(string name)
        {
            var activityTask = new ActivityTask(name, HttpContext.Current.User.Identity.Name);

            _context.ActivityTasks.Add(activityTask);

            _context.SaveChanges();

            return activityTask;
        }

        public TemplateTask CreateTemplateTask(NewTemplateTaskDto newTemplateTaskDto)
        {
            var templateTask = new TemplateTask(newTemplateTaskDto, HttpContext.Current.User.Identity.Name);

            _context.TemplateTasks.Add(templateTask);

            _context.SaveChanges();

            return templateTask;
        }

        public List<ActivityTask> GetActivityTasks()
        {
            return _context.ActivityTasks.ToList();
        }

        public async Task DeleteTemplate(long id)
        {
            ActivityTemplate template = await _context.ActivityTemplates.FindAsync(id);

            _context.ActivityTemplates.Remove(template);

            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}