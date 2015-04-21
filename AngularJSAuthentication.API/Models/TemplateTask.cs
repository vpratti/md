using System;
using System.Collections.Generic;
using AngularJSAuthentication.API.Dto;

namespace AngularJSAuthentication.API.Models
{
    public class TemplateTask : EntityBase
    {
        public TemplateTask() { }

        public TemplateTask(NewTemplateTaskDto newTemplateTask, string username)
        {
            TemplateId = newTemplateTask.TemplateId;
            TaskId = newTemplateTask.TaskId;
            Stage = newTemplateTask.Stage;
            Environment = newTemplateTask.Environment;
            Domain = newTemplateTask.Domain;
            Tminus = newTemplateTask.Tminus;
            CreatedBy = username;
            ModifiedBy = username;
            CreatedOn = DateTime.UtcNow;
            ModifiedOn = DateTime.UtcNow;
            Active = true;
        }

        public int TemplateId { get; set; }
        public int TaskId { get; set; }
        public string Stage { get; set; }
        public string Environment { get; set; }
        public string Domain { get; set; }
        public int Tminus { get; set; }

        public virtual ICollection<ActivityTemplate> ActivityTemplates { get; set; }
        public virtual ICollection<ActivityTask> ActivityTasks { get; set; }

    }
}