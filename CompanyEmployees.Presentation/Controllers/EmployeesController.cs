using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObject_DTO_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployees.Presentation.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    public  class EmployeesController :ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public EmployeesController( IServiceManager serviceManager)  //injecting IServiceManager Interface
        {
            _serviceManager = serviceManager;
        }


           // As you can see, we have the
          // companyId parameter in our action and this parameter will be mapped
         //  from the main route. For that reason, we didn’t place it in the[HttpGet]
        // attribute as we did with the GetCompany action
        [HttpGet]
        public async Task<IActionResult> GetEmployeeForCompanyAsync(Guid companyId)
        {
            var employees = await _serviceManager.EmployeeService.GetEmployeesAsync(companyId,trackChanges:false);

            return Ok(employees);
        }


        [HttpGet("{id:guid}", Name = "GetEmployeeForCompany")]
        public async Task<IActionResult> GetEmployeeAsync(Guid companyId , Guid Id)
        {
            var employee = await _serviceManager.EmployeeService.GetEmployeeAsync(companyId, Id, trackChanges: false)
;
            return Ok(employee);



        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeForCompanyAsync(Guid companyId, [FromBody] EmployeeForCreationDto employee)

        {
            if (employee is null)
                return BadRequest("EmployeeForCreationDto object is null");

            var employeeToReturn =
           await _serviceManager.EmployeeService.CreateEmployeeForCompanyAsync(companyId, employee, trackChanges: false);


            return CreatedAtRoute("GetEmployeeForCompany", new
            {
                companyId,
                id = employeeToReturn.Id

            },
             employeeToReturn);
        }

        //Delete Employee
        [HttpDelete("{id:guid}")]
        public  IActionResult DeleteEmployeeForCompanyAsync(Guid companyId, Guid id)
        {
            _serviceManager.EmployeeService.DeleteEmployeeForCompany(companyId, id, trackChanges:
            false);
            return NoContent();
        }


        //updateEmployee
        [HttpPut("{id:guid}")]
        public IActionResult UpdateEmployeeForCompanyAsync(Guid companyId, Guid id, [FromBody] EmployeeForUpdateDto employee)

        {
            if (employee is null)
                return BadRequest("EmployeeForUpdateDto object is null");

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            _serviceManager.EmployeeService.UpdateEmployeeForCompany(companyId, id, employee, compTrackChange: false, empTrackChanges: true);

            return NoContent();
        }


    }
}
  