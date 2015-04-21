using System;
using System.Collections.Generic;
using AngularJSAuthentication.API.Dto;

namespace AngularJSAuthentication.API.Models
{
    public class ActivityTask : EntityBase
    {
        public ActivityTask() { }

        public ActivityTask(NewTimeframeDto newTimeframeDto, string username)
        {
            Name = newTimeframeDto.Name;
            Description = newTimeframeDto.Description;
            EndDate = newTimeframeDto.EndDate;
            CreatedBy = username;
            ModifiedBy = username;
            CreatedOn = DateTime.UtcNow;
            ModifiedOn = DateTime.UtcNow;
            Active = true;

            if (newTimeframeDto.ParentTimeFrame != null)
            {
                ParentId = newTimeframeDto.ParentTimeFrame.Id;
            }
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime EndDate { get; set; }

        public long? ParentId { get; set; }

        public virtual ActivityTask Parent { get; set; }

        public virtual ICollection<ActivityTask> SubTasks { get; set; } 
    }
}