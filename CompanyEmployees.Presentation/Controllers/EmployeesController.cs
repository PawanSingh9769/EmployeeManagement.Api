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
        public IActionResult GetEmployeeForCompany(Guid companyId)
        {
            var employees = _serviceManager.EmployeeService.GetEmployees(companyId,trackChanges:false);

            return Ok(employees);
        }


        [HttpGet("{id:guid}", Name = "GetEmployeeForCompany")]
        public IActionResult GetEmployee(Guid companyId , Guid Id)
        {
            var employee = _serviceManager.EmployeeService.GetEmployee(companyId, Id, trackChanges: false)
;
            return Ok(employee);



        }

        [HttpPost]
        public IActionResult CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDto employee)

        {
            if (employee is null)
                return BadRequest("EmployeeForCreationDto object is null");

            var employeeToReturn =
           _serviceManager.EmployeeService.CreateEmployeeForCompany(companyId, employee, trackChanges: false);


            return CreatedAtRoute("GetEmployeeForCompany", new
            {
                companyId,
                id = employeeToReturn.Id

            },
             employeeToReturn);
        }

        //Delete Employee
        [HttpDelete("{id:guid}")]
        public IActionResult DeleteEmployeeForCompany(Guid companyId, Guid id)
        {
            _serviceManager.EmployeeService.DeleteEmployeeForCompany(companyId, id, trackChanges:
            false);
            return NoContent();
        }

    }
}
