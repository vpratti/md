using System;
using System.Collections.Generic;
using System.Linq;

namespace AngularJSAuthentication.API.Models
{
    public class TimeframeDto
    {
        public TimeframeDto() { }

        public TimeframeDto(Timeframe timeframe)
        {
            Name = timeframe.Name;
            Description = timeframe.Description;
            EndDate = timeframe.EndDate;
            Id = timeframe.Id;
            ChildTimeframeDtos = null;
        }

        public string Name { get; set; }

        public string Label
        {
            get { return Name; }
        }

        public string Description { get; set; }

        public DateTime EndDate { get; set; }

        public long Id { get; set; }

        public List<TimeframeDto> ChildTimeframeDtos { get; set; }

        public List<Item> Children
        {
            get
            {
                var children = new List<Item>();

                if (ChildTimeframeDtos != null && ChildTimeframeDtos.Any())
                {
                    ChildTimeframeDtos.ForEach(i => children.Add(new Item(i.Name, i.Id)));
                }

                return children;
            }
        }
    }
}