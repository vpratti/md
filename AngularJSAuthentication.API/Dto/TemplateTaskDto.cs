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

        public long Id { get; set; }
        public long TemplateId { get; set; }
        public long? TaskId { get; set; }
        public string Stage { get; set; }
        public string Environment { get; set; }
        public string Domain { get; set; }
        public string TaskName { get; set; }
        public long Tminus { get; set; }
        public bool Milestone { get; set; }
        public string MilestoneGroup { get; set; }

        public ActivityTaskDto ActivityTask { get; set; }
    }
}