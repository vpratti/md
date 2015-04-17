using System;
using System.Collections.Generic;
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
                .IgnoreAllNonExisting();

            Mapper.CreateMap<LookupValueDto, LookupValue>()
                .IgnoreAllNonExisting();

            Mapper.CreateMap<CategoryDto, Category>()
                .ForMember(dest => dest.LookupValues, opt => opt.ResolveUsing(new LookupValuesResovler()))
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
            //throw new NotImplementedException();
        }
    }
}