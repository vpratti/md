using System;

namespace AngularJSAuthentication.API.Dto
{
    public class NewTemplateTaskDto
    {
        public int TemplateId { get; set; }
        public int TaskId { get; set; }
        public string Stage { get; set; }
        public string Environment { get; set; }
        public string Domain { get; set; }
        public int Tminus { get; set; }
    }
}