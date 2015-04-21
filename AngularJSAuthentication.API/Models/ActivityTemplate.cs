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
            CreatedBy = username;
            ModifiedBy = username;
            CreatedOn = DateTime.UtcNow;
            ModifiedOn = DateTime.UtcNow;
            Active = true;
        }

        public string Name { get; set; }

        public virtual ICollection<TemplateTask> TemplateTasks { get; set; }     
    }
}