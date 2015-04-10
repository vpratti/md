using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AngularJSAuthentication.API.Constants;
using AngularJSAuthentication.API.Models;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using Seterlund.CodeGuard;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/Lookup")]
    public class LookupsController : ApiController
    {
        private readonly ILookupRepository _lookupRepository;
        private readonly IMappingEngine _mappingEngine;

        public LookupsController() : this(new LookupRepository(), Mapper.Engine) { }

        public LookupsController(ILookupRepository lookupRepository, IMappingEngine mappingEngine)
        {
            _lookupRepository = lookupRepository;
            _mappingEngine = mappingEngine;
        }

        [HttpGet]
        [Authorize(Roles = UserConstants.Admin)]
        [Route("GetCategories")]
        public async Task<IHttpActionResult> GetCategories(long id)
        {
            var result = _lookupRepository.GetCategories(id);

            List<CategoryDto> mappedResult = _mappingEngine.Map<List<Category>, List<CategoryDto>>(result);

            return Ok(mappedResult);
        }

        [HttpPost]
        [Authorize(Roles = UserConstants.Admin)]
        [Route("CreateCategory")]
        public async Task<IHttpActionResult> CreateCategory(CategoryDto category)
        {
            Guard.That(category.Code).IsNotNull();
            Guard.That(category.Description).IsNotNull();
            Guard.That(category.Type).IsNotNull();

            var mappedCategory = new Category(category.Code, category.Description, category.Type.Id);
            
            _lookupRepository.CreateCategory(mappedCategory);

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = UserConstants.Admin)]
        [Route("EditCategory")]
        public async Task<IHttpActionResult> EditCategory(CategoryDto category)
        {
            Guard.That(category.Code).IsNotNull();
            Guard.That(category.Description).IsNotNull();
            Guard.That(category.Type).IsNotNull();

            var mappedCategory = new Category(category.Code, category.Description, category.Type.Id, category.Id);

            _lookupRepository.EditCategory(mappedCategory);

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = UserConstants.Admin)]
        [Route("CreateCategoryType")]
        public async Task<IHttpActionResult> CreateCategoryType(string name)
        {
           _lookupRepository.CreateCategoryType(name);

            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = UserConstants.Admin)]
        [Route("GetCategoryTypes")]
        public async Task<IHttpActionResult> GetCategoryTypes()
        {
            List<CategoryType> categoryTypes = _lookupRepository.GetCategoryTypes();

            List<CategoryTypeDto> categoryTypeDtos =
                _mappingEngine.Map<List<CategoryType>, List<CategoryTypeDto>>(categoryTypes);

            return Ok(categoryTypeDtos);
        }

        [HttpDelete]
        [Authorize(Roles = UserConstants.Admin)]
        [Route("DeleteCategory")]
        public async Task<IHttpActionResult> DeleteCategory(long id)
        {
           _lookupRepository.DeleteCategory(id);

            return Ok();
        }
    }
}