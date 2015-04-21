using System;
using System.Collections.Generic;
using AngularJSAuthentication.API.Dto;

namespace AngularJSAuthentication.API.Models
{
    public class ActivityTask : EntityBase
    {
        public ActivityTask() { }

        public ActivityTask(NewActivityTaskDto newActivityTaskDto, string username)
        {
            Name = newActivityTaskDto.Name;
            CreatedBy = username;
            ModifiedBy = username;
            CreatedOn = DateTime.UtcNow;
            ModifiedOn = DateTime.UtcNow;
            Active = true;
        }

        public ActivityTask(string name, string username)
        {
            Name = name;
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