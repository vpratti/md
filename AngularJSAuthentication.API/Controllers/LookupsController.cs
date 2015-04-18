using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AngularJSAuthentication.API.Constants;
using AngularJSAuthentication.API.Models;
using AutoMapper;
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
        public async Task<IHttpActionResult> GetCategories()
        {
            var result = _lookupRepository.GetCategories();

            List<CategoryDto> mappedResult = _mappingEngine.Map<List<Category>, List<CategoryDto>>(result);

            return Ok(mappedResult);
        }

        [HttpPost]
        [Authorize(Roles = UserConstants.Admin)]
        [Route("CreateCategory")]
        public async Task<IHttpActionResult> CreateCategory(CategoryDto categoryDto)
        {
            Guard.That(categoryDto.Code).IsNotNull();
            Guard.That(categoryDto.Description).IsNotNull();

            _lookupRepository.CreateCategory(categoryDto);

            //_lookupRepository.UpdateCategory()
            //var category = _mappingEngine.Map<CategoryDto, Category>();
            //var mappedCategory = new Category(category.Code, category.Description, category.ValuesDtos.Id);
            
            //_lookupRepository.CreateCategory(mappedCategory);

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = UserConstants.Admin)]
        [Route("EditCategory")]
        public async Task<IHttpActionResult> EditCategory(CategoryDto category)
        {
            Guard.That(category.Code).IsNotNull();
            Guard.That(category.Description).IsNotNull();
            Guard.That(category.Values).IsNotNull();

            //var mappedCategory = new Category(category.Code, category.Description, category.Type.Id, category.Id);

            //_lookupRepository.EditCategory(mappedCategory);

            return Ok();
        }

        //[HttpPut]
        //[Authorize(Roles = UserConstants.Admin)]
        //[Route("EditCategoryType")]
        //public async Task<IHttpActionResult> EditCategoryType(LookupValueDto lookupValue)
        //{
        //    Guard.That(lookupValue.Name).IsNotNull();

        //    _lookupRepository.EditCategoryType(lookupValue.Id, lookupValue.Name);

        //    return Ok();
        //}

        //[HttpPost]
        //[Authorize(Roles = UserConstants.Admin)]
        //[Route("CreateCategoryType")]
        //public async Task<IHttpActionResult> CreateCategoryType(string name)
        //{
        //   _lookupRepository.CreateCategoryType(name);

        //    return Ok();
        //}

        [HttpPost]
        [Authorize(Roles = UserConstants.Admin)]
        [Route("CreateLookupAlias")]
        public async Task<IHttpActionResult> CreateLookupAlias(LookupAliasDto lookupAliasDto)
        {
            _lookupRepository.CreateLookupAlias(lookupAliasDto);

            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = UserConstants.Admin)]
        [Route("GetLookupValues")]
        public async Task<IHttpActionResult> GetLookupValues()
        {
            List<LookupValue> lookupValues = _lookupRepository.GetLookupValues();

            List<LookupValueDto> categoryTypeDtos =
                _mappingEngine.Map<List<LookupValue>, List<LookupValueDto>>(lookupValues);

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