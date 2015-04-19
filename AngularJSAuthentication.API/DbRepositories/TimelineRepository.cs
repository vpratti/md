using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AngularJSAuthentication.API.DbContexts;
using AngularJSAuthentication.API.Models;

namespace AngularJSAuthentication.API.DbRepositories
{
    public class TimelineRepository : ITimelineRepository, IDisposable
    {
        private readonly LookupContext _lookupContext;

        public TimelineRepository()
        {
            _lookupContext = new LookupContext();
        }

        public Timeframe CreateTimeframe(NewTimeframeDto newTimeframeDto)
        {
            var timeframe = new Timeframe(newTimeframeDto, HttpContext.Current.User.Identity.Name);

            _lookupContext.Timeframes.Add(timeframe);

            _lookupContext.SaveChanges();

            return timeframe;
        }

        public List<Timeframe> GetTimeframes()
        {
            return _lookupContext.Timeframes.Where(i => i.ParentId == null).ToList();
        }

        public void Dispose()
        {
            _lookupContext.Dispose();
        }
    }
}