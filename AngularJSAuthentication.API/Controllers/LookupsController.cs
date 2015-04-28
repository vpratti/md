using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AngularJSAuthentication.API.Constants;
using AngularJSAuthentication.API.DbRepositories;
using AngularJSAuthentication.API.Dto;
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

            _lookupRepository.EditCategory(category);

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = UserConstants.Admin)]
        [Route("CreateLookupValue")]
        public async Task<IHttpActionResult> CreateLookupValue(LookupValueDto lookupValueDto)
        {
            LookupValue result = _lookupRepository.CreateLookupValue(lookupValueDto);

            LookupValueDto mappedResult = _mappingEngine.Map<LookupValue, LookupValueDto>(result);

            return Ok(mappedResult);
        }

        [HttpPost]
        [Authorize(Roles = UserConstants.Admin)]
        [Route("CreateLookupAlias")]
        public async Task<IHttpActionResult> CreateLookupAlias(LookupAliasDto lookupAliasDto)
        {
            Guard.That(lookupAliasDto.Name).IsNotNullOrEmpty();

            LookupAlias result = _lookupRepository.CreateLookupAlias(lookupAliasDto);

            LookupAliasDto mappedResult = _mappingEngine.Map<LookupAlias, LookupAliasDto>(result);

            return Ok(mappedResult);
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

        [HttpGet]
        [Authorize]
        [Route("GetLookupsByCategoryCode")]
        public async Task<IHttpActionResult> GetLookupsByCategoryCode(string code)
        {
            List<LookupValue> result = await _lookupRepository.GetLookupValuesByCategoryCode(code);

            result = result.Where(i => i.Active).ToList();

            List<LookupValueDto> mappedResult = _mappingEngine.Map<List<LookupValue>, List<LookupValueDto>>(result);

            return Ok(mappedResult);
        }

        [HttpDelete]
        [Authorize(Roles = UserConstants.Admin)]
        [Route("DeleteCategory")]
        public async Task<IHttpActionResult> DeleteCategory(long id)
        {
           _lookupRepository.DeleteCategory(id);

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = UserConstants.Admin)]
        [Route("DeleteAlias")]
        public async Task<IHttpActionResult> DeleteAlias(long id)
        {
            _lookupRepository.DeleteAlias(id);

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = UserConstants.Admin)]
        [Route("DeleteLookupValue")]
        public async Task<IHttpActionResult> DeleteLookupValue(long id)
        {
            _lookupRepository.DeleteLookupValue(id);

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = UserConstants.Admin)]
        [Route("EditLookupValue")]
        public async Task<IHttpActionResult> EditLookupValue(LookupValueDto lookupValueDto)
        {
            Guard.That(lookupValueDto.Name).IsNotNullOrEmpty();

            _lookupRepository.EditLookupValue(lookupValueDto);

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = UserConstants.Admin)]
        [Route("EditAlias")]
        public async Task<IHttpActionResult> EditAlias(LookupAliasDto lookupAliasDto)
        {
            Guard.That(lookupAliasDto.Name).IsNotNullOrEmpty();

            _lookupRepository.EditAlias(lookupAliasDto);

            return Ok();
        }
    }
}