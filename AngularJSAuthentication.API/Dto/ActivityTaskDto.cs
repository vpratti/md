using AngularJSAuthentication.API.Models;

namespace AngularJSAuthentication.API.Dto
{
    public class ActivityTaskDto
    {
        public ActivityTaskDto() { }

        public ActivityTaskDto(ActivityTask activityTask)
        {
            Id = activityTask.Id;
            Name = activityTask.Name;
        }

        public long Id { get; set; }

        public string Name { get; set; }
       
    }
}