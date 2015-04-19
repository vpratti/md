using System;
using System.Collections.Generic;

namespace AngularJSAuthentication.API.Models
{
    public class Timeframe : EntityBase
    {
        public Timeframe() { }

        public Timeframe(NewTimeframeDto newTimeframeDto, string username)
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

        public DateTime EndDate { get; set; }

        public long? ParentId { get; set; }

        public virtual Timeframe Parent { get; set; }

        public virtual ICollection<Timeframe> Children { get; set; } 
    }
}