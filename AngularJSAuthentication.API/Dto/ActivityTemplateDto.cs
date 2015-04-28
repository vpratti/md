using System.Collections.Generic;
using AngularJSAuthentication.API.Models;

namespace AngularJSAuthentication.API.Dto
{
    public class AcvitityTemplateDto
    {
        public AcvitityTemplateDto() { }

        public AcvitityTemplateDto(ActivityTemplate activityTemplate)
        {
            Id = activityTemplate.Id;
            Name = activityTemplate.Name;
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public string Stage { get; set; }

        public string Environment { get; set; }

        public string Domain { get; set; }

        public bool Active { get; set; }

        public List<TemplateTaskDto> TemplateTasks { get; set; }  
    }
}