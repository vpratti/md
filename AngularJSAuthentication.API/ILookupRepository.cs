
using System.Collections.Generic;
using AngularJSAuthentication.API.Models;

namespace AngularJSAuthentication.API
{
    public interface ILookupRepository
    {
        void CreateCategoryType(string name);

        List<CategoryType> GetCategoryTypes();

        void CreateCategory(Category category);

        List<Category> GetCategories(long typeId);

        void DeleteCategory(long id);

        void EditCategory(Category category);
    }
}