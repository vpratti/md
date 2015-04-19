using System.Collections.Generic;
using System.Linq;
using AngularJSAuthentication.API.Extensions;
using AngularJSAuthentication.API.Models;
using AutoMapper;
using WebGrease.Css.Extensions;

namespace AngularJSAuthentication.API.Mapping
{
    public class TimelinesProfile : Profile
    {
        protected override void Configure()
        {
            base.Configure();

            Mapper.CreateMap<Timeframe, TimeframeDto>()
                .ForMember(dest => dest.ChildTimeframeDtos, opt => opt.ResolveUsing(new TimeframeDtoChildrenResolver()))
                .ForMember(dest => dest.Children, opt => opt.Ignore())
                .IgnoreAllNonExisting();
        }
    }

    public class TimeframeDtoChildrenResolver : ValueResolver<Timeframe, List<TimeframeDto>>
    {
        protected override List<TimeframeDto> ResolveCore(Timeframe source)
        {
            var children = new List<TimeframeDto>();

            if (source.Children != null && source.Children.Any())
            {
                source.Children.ForEach(i => children.Add(new TimeframeDto(i)));
            }

            return children;
        }
    }
}