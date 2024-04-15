using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObject_DTO_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyEmployees.Presentation.ModelBinders;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;

namespace CompanyEmployees.Presentation.Controllers
{
    [Route("api/companies")]
    [ApiController ]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _services;
        public CompaniesController(IServiceManager services) //injecting IServiceManager Interface
        {
            _services = services;
        }

        [HttpGet]                                          //decorating GetCompanies action with HTTP Get attribute
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetCompaniesAsync()               // IActionResult which return not only the result but also status code
        {

            //throw new Exception("Exception");
            //we use injected service to call the service method that gets the data from repository clas
            var companies = await _services.CompanyService.GetAllCompaniesAsync(trackChanges: false);
            return Ok(companies);
        }

        [HttpGet("{id:guid}",Name = "CompanyById")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public , MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task< IActionResult> getCompany(Guid id)
        {
            var company = await _services.CompanyService.GetCompanyAsync(id, trackChanges: false);
            return Ok(company);
        }

        [HttpPost]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      
        public async Task<IActionResult> CreateCompanyAsync([FromBody] CompanyForCreationDto company)
        {
            if (company is null)
               return BadRequest("CompanyForCreationDto object is null");

            var createdCompany = await _services.CompanyService.CreateCompanyAsync(company);

            return CreatedAtRoute("CompanyById", new { id = createdCompany.Id },
            createdCompany);
        }


        // check why we need this requests once more

        [HttpGet("collection/({ids})", Name = "CompanyCollection")]
        public async Task<IActionResult> GetCompanyCollectionAync([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)

        {
            var companies = await _services.CompanyService.GetByIdsAsync(ids,trackChanges: false);

            return Ok(companies);
        }


        [HttpPost("collection")]
        public async Task<IActionResult> CreateCompanyCollectionAsync([FromBody] IEnumerable<CompanyForCreationDto> companyCollection)

        {
            var result = await _services.CompanyService.CreateCompanyCollectionAsync(companyCollection);
            return CreatedAtRoute("CompanyCollection", new { result.ids },
            result.companies);
        }
    }
}
