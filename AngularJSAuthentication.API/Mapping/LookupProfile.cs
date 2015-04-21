using System.Collections.Generic;
using AngularJSAuthentication.API.Dto;
using AngularJSAuthentication.API.Extensions;
using AngularJSAuthentication.API.Models;
using AutoMapper;

namespace AngularJSAuthentication.API.Mapping
{
    public class LookupProfile : Profile
    {
        protected override void Configure()
        {
            base.Configure();

            Mapper.CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Values, opt => opt.ResolveUsing(new LookupValuesDtoResolver()))
                .IgnoreAllNonExisting();

            Mapper.CreateMap<LookupValue, LookupValueDto>()
                .ForMember(dest => dest.LookupAliases, opt => opt.ResolveUsing(new LookupAliasesDtoResolver()))
                .IgnoreAllNonExisting();

            Mapper.CreateMap<LookupValueDto, LookupValue>()
                .IgnoreAllNonExisting();

            Mapper.CreateMap<CategoryDto, Category>()
                .ForMember(dest => dest.LookupValues, opt => opt.ResolveUsing(new LookupValuesResovler()))
                .IgnoreAllNonExisting();

            Mapper.CreateMap<LookupAlias, LookupAliasDto>()
                .IgnoreAllNonExisting();
        }
    }


    public class LookupValuesResovler : ValueResolver<CategoryDto, List<LookupValue>>
    {
        protected override List<LookupValue> ResolveCore(CategoryDto source)
        {
            var lookupValues = new List<LookupValue>();

            source.Values.ForEach(i => lookupValues.Add(new LookupValue(i.Name, i.Active, i.CategoryId)));

            return lookupValues;
        }
    }

    public class LookupValuesDtoResolver : ValueResolver<Category, List<LookupValueDto>>
    {
        protected override List<LookupValueDto> ResolveCore(Category source)
        {
            return Mapper.Map<List<LookupValue>, List<LookupValueDto>>(source.LookupValues);
        }
    }

    public class LookupAliasesDtoResolver : ValueResolver<LookupValue, List<LookupAliasDto>>
    {
        protected override List<LookupAliasDto> ResolveCore(LookupValue source)
        {
            return Mapper.Map<List<LookupAlias>, List<LookupAliasDto>>(source.LookupAliases);
        }
    }
}