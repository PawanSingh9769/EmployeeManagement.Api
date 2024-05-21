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

        

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(Guid companyId, bool trackChnages)
        {
            var company = await _repository.Company.GetCompanyAsync(companyId, trackChnages);
            if (company == null)
            {
                throw new CompanyNotFoundException(companyId);
            }
            var employeesFromDb = await _repository.Employee.GetEmployeesAsync(companyId, trackChnages);

            var employeeDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);
            return employeeDto;

        }

        public async Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid Id, bool trackChanges)
        {
            var company = _repository.Company.GetCompanyAsync(companyId, trackChanges);
            if (company == null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            var employeeDb = await _repository.Employee.GetEmployeeAsync(companyId, Id, trackChanges);
            if(employeeDb == null)
            {
                throw new EmployeeNotFoundException(Id);
            }

            var employee = _mapper.Map<EmployeeDto>(employeeDb);
            return employee;
          
        }

        public async Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreationDto employeeForCreation, bool trackChanges)
        {
            var company = _repository.Company.GetCompanyAsync(companyId, trackChanges);
            if(company == null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            var employeeEntity = _mapper.Map<Employee>(employeeForCreation);   //mapping Dto to an entity
            _repository.Employee.CreateEmployeeForCompany(companyId , employeeEntity); // call the repo method to creatre new employee
           await _repository.SaveAsync();

            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity); // map back the entity to Dto 
            return employeeToReturn;
        }

        


        //update 

        public void UpdateEmployeeForCompany(Guid companyId, Guid id, EmployeeForUpdateDto employeeForUpdate, bool compTrackChange, bool empTrackChanges)
        {

            var company = _repository.Company.GetCompanyAsync(companyId, compTrackChange);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employeeEntity = _repository.Employee.GetEmployeeAsync(companyId, id, empTrackChanges);


            if (employeeEntity is null)
                throw new EmployeeNotFoundException(id);

            _mapper.Map(employeeForUpdate, employeeEntity);
            _repository.SaveAsync();
        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
       /* public void DeleteEmployeeForCompany(Guid companyId, Guid id, bool trackChanges)
        {
            var company = _repository.Company.GetCompanyAsync(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employeeForCompany = _repository.Employee.GetEmployeeAsync(companyId, id, trackChanges);

            if (employeeForCompany is null)
                throw new EmployeeNotFoundException(id);

            //  _repository.Employee.DeleteEmployee(employeeForCompany);
            _repository.Employee.DeleteEmployee(employeeForCompany);
             _repository.Save();
        }*/

        public async void DeleteEmployeeForCompany(Guid companyId, Guid id, bool trackChanges)
        {
            var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employeeForCompany = await _repository.Employee.GetEmployeeAsync(companyId, id, trackChanges);

            if (employeeForCompany is null)
                throw new EmployeeNotFoundException(id);

            _repository.Employee.DeleteEmployee(employeeForCompany);
            await _repository.SaveAsync(); // Assuming SaveAsync is an asynchronous method
        }
    }



}
