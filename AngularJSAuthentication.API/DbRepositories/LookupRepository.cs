using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AngularJSAuthentication.API.DbContexts;
using AngularJSAuthentication.API.Models;

namespace AngularJSAuthentication.API.DbRepositories
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

        public List<LookupValue> GetLookupValues()
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

        public void EditCategory(CategoryDto categoryDto)
        {
            var category = _context.Categories.Find(categoryDto.Id);

            category.Code = categoryDto.Code;
            category.Description = categoryDto.Description;
            category.Active = categoryDto.Active;
            category.ModifiedOn = DateTime.UtcNow;
            category.ModifiedBy = HttpContext.Current.User.Identity.Name;

            _context.SaveChanges();
        }

        public LookupAlias CreateLookupAlias(LookupAliasDto lookupAliasDto)
        {
            var lookupAlias = new LookupAlias(lookupAliasDto, HttpContext.Current.User.Identity.Name);

            LookupValue lookupValue = _context.LookupValues.Find(lookupAliasDto.LookupValueId);

            lookupValue.LookupAliases.Add(lookupAlias);
            
            _context.SaveChanges();

            return lookupAlias;
        }

        public LookupValue CreateLookupValue(LookupValueDto lookupValueDto)
        {
            var lookupValue = new LookupValue(lookupValueDto, HttpContext.Current.User.Identity.Name);

            Category category = _context.Categories.Find(lookupValue.CategoryId);

            category.LookupValues.Add(lookupValue);

            _context.SaveChanges();

            return lookupValue;
        }

        public void DeleteAlias(long id)
        {
            LookupAlias lookupAlias = _context.LookupAliases.Find(id);

            _context.LookupAliases.Remove(lookupAlias);

            _context.SaveChanges();
        }

        public void DeleteLookupValue(long id)
        {
            LookupValue lookupValue = _context.LookupValues.Find(id);

            _context.LookupValues.Remove(lookupValue);

            _context.SaveChanges();
        }

        public void EditLookupValue(LookupValueDto lookupValueDto)
        {
            LookupValue lookupValue = _context.LookupValues.Find(lookupValueDto.Id);

            lookupValue.Name = lookupValueDto.Name;
            lookupValue.Active = lookupValueDto.Active;
            lookupValue.ModifiedOn = DateTime.UtcNow;
            lookupValue.ModifiedBy = HttpContext.Current.User.Identity.Name;

            _context.SaveChanges();
        }

        public void EditAlias(LookupAliasDto lookupAliasDto)
        {
            LookupAlias lookupAlias = _context.LookupAliases.Find(lookupAliasDto.Id);

            lookupAlias.Name = lookupAliasDto.Name;
            lookupAlias.Active = lookupAliasDto.Active;
            lookupAlias.ModifiedOn = DateTime.UtcNow;
            lookupAlias.ModifiedBy = HttpContext.Current.User.Identity.Name;

            _context.SaveChanges();
        }
    }
}