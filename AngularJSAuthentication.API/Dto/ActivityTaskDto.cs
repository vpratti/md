using System;
using System.Collections.Generic;
using System.Linq;
using AngularJSAuthentication.API.Models;

namespace AngularJSAuthentication.API.Dto
{
    public class TaskDto
    {
        public TaskDto() { }

        public TaskDto(ActivityTask task)
        {
            Id = task.Id;
            Name = task.Name;
            Description = task.Description;
            StartDate = task.StartDate;
            EndDate = task.EndDate;
            EndDate = task.EndDate;
            SubTaskDtos = null;
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public string Label
        {
            get { return Name; }
        }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<TaskDto>SubTaskDtos { get; set; }

        public List<Item> SubTasks
        {
            get
            {
                var children = new List<Item>();

                if (SubTaskDtos != null && SubTaskDtos.Any())
                {
                    SubTaskDtos.ForEach(i => children.Add(new Item(i.Name, i.Id)));
                }

                return children;
            }
        }
    }
}