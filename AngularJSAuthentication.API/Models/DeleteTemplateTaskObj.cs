namespace AngularJSAuthentication.API.Models
{
    public class DeleteTemplateTaskObj
    {
        public DeleteTemplateTaskObj() { }

        public DeleteTemplateTaskObj(long taskid, long templateid)
        {
            TaskId = taskid;
            TemplateId = templateid;
        }

        public long TaskId { get; set; }
        public long TemplateId { get; set; }
    }
}