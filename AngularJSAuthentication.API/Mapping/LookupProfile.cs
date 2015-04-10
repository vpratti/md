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
                .ForMember(dest => dest.Type, opt => opt.ResolveUsing(new CategoryTypeDtoResolver()))
                .IgnoreAllNonExisting();

            Mapper.CreateMap<CategoryType, CategoryTypeDto>()
                .IgnoreAllNonExisting();
        }
    }


    public class CategoryTypeDtoResolver : ValueResolver<Category, CategoryTypeDto>
    {
        protected override CategoryTypeDto ResolveCore(Category source)
        {
            return Mapper.Map<CategoryType, CategoryTypeDto>(source.Type);
        }
    }
}