using System;
using System.Collections.Generic;
using AngularJSAuthentication.API.Dto;
using WebGrease.Css.Extensions;

namespace AngularJSAuthentication.API.Models
{
    public class ActivityTemplate : EntityBase
    {
        public ActivityTemplate() { }

        public ActivityTemplate(NewActivityTemplateDto newActivityTemplate, string username)
        {
            Name = newActivityTemplate.Name;
            Stage = newActivityTemplate.Stage;
            Environment = newActivityTemplate.Environment;
            Domain = newActivityTemplate.Domain;
            CreatedBy = username;
            ModifiedBy = username;
            CreatedOn = DateTime.UtcNow;
            ModifiedOn = DateTime.UtcNow;
            Active = true;
        }

        public string Name { get; set; }
        public string Stage { get; set; }
        public string Environment { get; set; }
        public string Domain { get; set; }

        public virtual ICollection<TemplateTask> TemplateTasks { get; set; }

        public void UpdateActivityTemplate(ActivityTemplateDto activityTemplateDto, string username)
        {
            Name = activityTemplateDto.Name;

            SyncTemplateTasks(activityTemplateDto);

            ModifiedOn = DateTime.UtcNow;
            ModifiedBy = username;
        }

        private void SyncTemplateTasks(ActivityTemplateDto activityTemplateDto)
        {
            if (Stage != activityTemplateDto.Stage && !string.IsNullOrEmpty(activityTemplateDto.Stage))
            {
                Stage = activityTemplateDto.Stage;
                TemplateTasks.ForEach(i => i.Stage = Stage);
            }
            if (Environment != activityTemplateDto.Environment && !string.IsNullOrEmpty(activityTemplateDto.Environment))
            {
                Environment = activityTemplateDto.Environment;
                TemplateTasks.ForEach(i => i.Environment = Environment);
            }
            if (Domain != activityTemplateDto.Domain && !string.IsNullOrEmpty(activityTemplateDto.Domain))
            {
                Domain = activityTemplateDto.Domain;
                TemplateTasks.ForEach(i => i.Domain = Domain);
            }
        }
    }
}