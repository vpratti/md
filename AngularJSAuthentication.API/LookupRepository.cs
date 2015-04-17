using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AngularJSAuthentication.API.Models;
using AutoMapper;

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
            if (_context.LookupValues.Any(i => i.Name.ToLower().Equals(name.ToLower())))
            {
                throw new Exception("Category type already exists.");
            }

            //_context.LookupValues.Add(new LookupValue(name));

            _context.SaveChanges();
        }

        public List<LookupValue> GetCategoryTypes()
        {
            return _context.LookupValues.ToList();
        }

        public void CreateCategory(CategoryDto categoryDto)
        {
            var category = new Category(categoryDto.Code, categoryDto.Description, HttpContext.Current.User.Identity.Name);

            var result = _context.Categories.Add(category);

            var lookupValues = new List<LookupValue>();

            categoryDto.Values.ForEach(i => lookupValues.Add(new LookupValue(i.Name, i.Active, result.Id, HttpContext.Current.User.Identity.Name)));

            result.LookupValues = lookupValues;

            _context.SaveChanges();
        }

        public List<Category> GetCategories()
        {
            var result = _context.Categories;

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
            //originalCategory.CategoryTypeId = category.CategoryTypeId;
            originalCategory.Active = category.Active;

            _context.SaveChanges();
        }

        public void EditCategoryType(long id, string name)
        {
            LookupValue category = _context.LookupValues.Find(id);

            if (category == null)
            {
                throw new Exception("There was an error while trying to edit your category");
            }

            if (_context.LookupValues.Any(i => i.Name.ToLower().Equals(name.ToLower()) && i.Id != category.Id))
            {
                throw new Exception("There is already a Type with that name");
            }

            category.Name = name;

            _context.SaveChanges();
        }

        //private Category SetCreateCategoryDefaults(CategoryDto categoryDto)
        //{
        //    var category = new Category {Active = true};

        //    var username = HttpContext.Current.User.Identity.Name;

        //    category.CreatedBy = username;
        //    category.ModifiedBy = username;
        //    category.CreatedOn = DateTime.UtcNow;
        //    category.ModifiedOn = DateTime.UtcNow;
        //    category.Code = categoryDto.Code;
        //    category.Description = categoryDto.Description;

        //    return category;
        //}
    }
}