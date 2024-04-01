using AutoMapper;
using Contracts;
using Entites.Exceptions;
using Entites.Models;
using Service.Contracts;
using Shared.DataTransferObject_DTO_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EmployeeService(IRepositoryManager repository, ILoggerManager
      logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        

        public IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChnages)
        {
            var company = _repository.Company.GetCompany(companyId, trackChnages);
            if (company == null)
            {
                throw new CompanyNotFoundException(companyId);
            }
            var employeesFromDb = _repository.Employee.GetEmployees(companyId, trackChnages);

            var employeeDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);
            return employeeDto;

        }

        public EmployeeDto GetEmployee(Guid companyId, Guid Id, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if (company == null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            var employeeDb = _repository.Employee.GetEmployee(companyId,Id, trackChanges);
            if(employeeDb == null)
            {
                throw new EmployeeNotFoundException(Id);
            }

            var employee = _mapper.Map<EmployeeDto>(employeeDb);
            return employee;
          
        }

        public EmployeeDto CreateEmployeeForCompany(Guid companyId, EmployeeForCreationDto employeeForCreation, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if(company == null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            var employeeEntity = _mapper.Map<Employee>(employeeForCreation);   //mapping Dto to an entity
            _repository.Employee.CreateEmployeeForCompany(companyId , employeeEntity); // call the repo method to creatre new employee
            _repository.Save();

            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity); // map back the entity to Dto 
            return employeeToReturn;
        }
    }



}
