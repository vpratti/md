using AngularJSAuthentication.API.Models;

namespace AngularJSAuthentication.API.Dto
{
    public class TaskDto
    {
        public TaskDto() { }

        public TaskDto(ActivityTask activityTask)
        {
            Id = activityTask.Id;
            Name = activityTask.Name;
        }

        public long Id { get; set; }

        public string Name { get; set; }
       
    }
}