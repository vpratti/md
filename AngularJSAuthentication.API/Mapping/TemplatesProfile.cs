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

            Mapper.CreateMap<ActivityTemplate, AcvitityTemplateDto>()
                .ForMember(dest => dest.TemplateTasks, opt => opt.ResolveUsing(new TemplateTasksDtoResolver()))
                .IgnoreAllNonExisting();

            Mapper.CreateMap<TemplateTask, TemplateTaskDto>()
                .IgnoreAllNonExisting();
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