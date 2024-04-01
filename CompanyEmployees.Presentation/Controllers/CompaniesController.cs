using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObject_DTO_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public IActionResult GetCompanies()               // IActionResult which return not only the result but also status code
        {

            //throw new Exception("Exception");
            //we use injected service to call the service method that gets the data from repository clas
            var companies = _services.CompanyService.GetAllCompanies(trackChanges: false);
            return Ok(companies);
        }

        [HttpGet("{id:guid}",Name = "CompanyById")]
        public IActionResult getCompany(Guid id)
        {
            var company = _services.CompanyService.GetCompany(id,trackChanges: false);
            return Ok(company);
        }

        [HttpPost] 
        public IActionResult CreateCompany([FromBody] CompanyForCreationDto company)
        {
            if (company is null)
                return BadRequest("CompanyForCreationDto object is null");

            var createdCompany = _services.CompanyService.CreateCompany(company);

            return CreatedAtRoute("CompanyById", new { id = createdCompany.Id },
            createdCompany);
        }
    }
}
