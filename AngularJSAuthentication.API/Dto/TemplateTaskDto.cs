using System;
using AngularJSAuthentication.API.Models;

namespace AngularJSAuthentication.API.Dto
{
    public class TemplateTaskDto
    {
        public TemplateTaskDto() { }

        public TemplateTaskDto(TemplateTask templateTask)
        {
            TemplateId = templateTask.TemplateId;
            TaskId = templateTask.TaskId;
            Stage = templateTask.Stage;
            Environment = templateTask.Environment;
            Domain = templateTask.Domain;
            Tminus = templateTask.Tminus;
        }

        public long TemplateId { get; set; }
        public long TaskId { get; set; }
        public string Stage { get; set; }
        public string Environment { get; set; }
        public string Domain { get; set; }
        public int Tminus { get; set; }
    }
}