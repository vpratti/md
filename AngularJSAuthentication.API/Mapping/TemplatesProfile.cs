using System.Collections.Generic;
using System.Linq;
using AngularJSAuthentication.API.Dto;
using AngularJSAuthentication.API.Extensions;
using AngularJSAuthentication.API.Models;
using AutoMapper;

namespace AngularJSAuthentication.API.Mapping
{
    public class TemplatesProfile : Profile
    {
        protected override void Configure()
        {
            base.Configure();

            Mapper.CreateMap<ActivityTemplate, ActivityTemplateDto>()
                .ForMember(dest => dest.TemplateTasks, opt => opt.ResolveUsing(new TemplateTasksDtoResolver()))
                .IgnoreAllNonExisting();

            Mapper.CreateMap<TemplateTask, TemplateTaskDto>()
                .ForMember(dest => dest.ActivityTask, opt => opt.ResolveUsing(new ActivityTaskDtoResolver()))
                .IgnoreAllNonExisting();

            Mapper.CreateMap<ActivityTask, ActivityTaskDto>()
                .IgnoreAllNonExisting();
        }
    }

    public class ActivityTaskDtoResolver : ValueResolver<TemplateTask, ActivityTaskDto>
    {
        protected override ActivityTaskDto ResolveCore(TemplateTask source)
        {
            return Mapper.Map<ActivityTask, ActivityTaskDto>(source.ActivityTask);
        }
    }

    public class TemplateTasksDtoResolver : ValueResolver<ActivityTemplate, List<TemplateTaskDto>>
    {
        protected override List<TemplateTaskDto> ResolveCore(ActivityTemplate source)
        {
            var templateTasks = new List<TemplateTaskDto>();

            if (source.TemplateTasks != null && source.TemplateTasks.Any())
            {
                templateTasks = Mapper.Map<List<TemplateTask>, List<TemplateTaskDto>>(source.TemplateTasks.ToList());
            }

            return templateTasks;
        }
    }
}