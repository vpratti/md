using System;

namespace AngularJSAuthentication.API.Dto
{
    public class NewTemplateTaskDto
    {
        public long TemplateId { get; set; }
        public long TaskId { get; set; }
        public string Stage { get; set; }
        public string Environment { get; set; }
        public string Domain { get; set; }
        public int Tminus { get; set; }
    }
}