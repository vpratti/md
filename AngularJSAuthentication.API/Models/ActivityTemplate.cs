using System;
using System.Collections.Generic;
using AngularJSAuthentication.API.Dto;

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
    }
}