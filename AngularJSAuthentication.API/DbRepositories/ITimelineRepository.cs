using System.Collections.Generic;
using AngularJSAuthentication.API.Models;

namespace AngularJSAuthentication.API.DbRepositories
{
    public interface ITimelineRepository
    {
        Timeframe CreateTimeframe(NewTimeframeDto newTimeframeDto);

        List<Timeframe> GetTimeframes();
    }
}