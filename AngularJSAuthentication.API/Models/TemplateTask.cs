using System;
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

        public long TemplateId { get; set; }
        public long? TaskId { get; set; }
        public string Stage { get; set; }
        public string Environment { get; set; }
        public string Domain { get; set; }
        public long Tminus { get; set; }
    }
}