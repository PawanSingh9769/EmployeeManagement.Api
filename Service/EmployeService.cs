using AutoMapper;
using Contracts;
using Entites.Exceptions;
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
    }



}
