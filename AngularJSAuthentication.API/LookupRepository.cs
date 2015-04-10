using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AngularJSAuthentication.API.Models;

namespace AngularJSAuthentication.API
{
    public class LookupRepository : IDisposable, ILookupRepository
    {
        private readonly LookupContext _context;

        public LookupRepository()
        {
            _context = new LookupContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void CreateCategoryType(string name)
        {
            if (_context.CategoryTypes.Any(i => i.Name.ToLower().Equals(name)))
            {
                throw new Exception("Category type already exists.");
            }

            _context.CategoryTypes.Add(new CategoryType(name));

            _context.SaveChanges();
        }

        public List<CategoryType> GetCategoryTypes()
        {
            return _context.CategoryTypes.ToList();
        }

        public void CreateCategory(Category category)
        {
            category = SetCreateCategoryDefaults(category);

            _context.Categories.Add(category);

            _context.SaveChanges();
        }

        public List<Category> GetCategories(long typeId)
        {
            var result =_context.Categories.Where(i => i.CategoryTypeId == typeId);

            return result.ToList();
        }

        public void DeleteCategory(long id)
        {
            var category = _context.Categories.Find(id);

            _context.Categories.Remove(category);

            _context.SaveChanges();
        }

        public void EditCategory(Category category)
        {
            var originalCategory = _context.Categories.Find(category.Id);

            originalCategory.Code = category.Code;
            originalCategory.Description = category.Description;
            originalCategory.CategoryTypeId = category.CategoryTypeId;
            originalCategory.Active = category.Active;

            _context.SaveChanges();
        }

        private Category SetCreateCategoryDefaults(Category category)
        {
            category.Active = true;

            var username = HttpContext.Current.User.Identity.Name;

            category.CreatedBy = username;
            category.ModifiedBy = username;
            category.CreatedOn = DateTime.UtcNow;
            category.ModifiedOn = DateTime.UtcNow;

            return category;
        }
    }
}