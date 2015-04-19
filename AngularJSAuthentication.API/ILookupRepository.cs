
using System.Collections.Generic;
using AngularJSAuthentication.API.Models;

namespace AngularJSAuthentication.API
{
    public interface ILookupRepository
    {
        List<LookupValue> GetLookupValues();

        void CreateCategory(CategoryDto categoryDto);

        List<Category> GetCategories();

        void DeleteCategory(long id);

        void EditCategory(CategoryDto categoryDto);

        LookupAlias CreateLookupAlias(LookupAliasDto lookupAliasDto);

        LookupValue CreateLookupValue(LookupValueDto lookupValueDto);

        void DeleteAlias(long id);

        void DeleteLookupValue(long id);

        void EditLookupValue(LookupValueDto lookupValueDto);

        void EditAlias(LookupAliasDto lookupAliasDto);
    }
}