
using System.Collections.Generic;
using AngularJSAuthentication.API.Models;

namespace AngularJSAuthentication.API
{
    public interface ILookupRepository
    {
        void CreateCategoryType(string name);

        List<LookupValue> GetCategoryTypes();

        void CreateCategory(CategoryDto categoryDto);

        List<Category> GetCategories();

        void DeleteCategory(long id);

        void EditCategory(Category category);

        void EditCategoryType(long id, string name);
    }
}