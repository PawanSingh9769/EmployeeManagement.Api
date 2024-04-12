using AutoMapper;

using Contracts;
using Entites.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICompanyService> _companyService;
        private readonly Lazy<IEmployeeService> _employeeService;
        private readonly Lazy<IAuthenticationService> _authenticationService; //JWT

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            _companyService = new Lazy<ICompanyService>(() => new
          CompanyService(repositoryManager, logger, mapper));
            _employeeService = new Lazy<IEmployeeService>(() => new
          EmployeeService(repositoryManager, logger, mapper));
            //JWT
            _authenticationService = new Lazy<IAuthenticationService>(() =>
                                 new AuthenticationService(logger, mapper, userManager, configuration));
        }


        public IAuthenticationService AuthenticationService => _authenticationService.Value;

        public ICompanyService CompanyService => _companyService.Value;
        public IEmployeeService EmployeeService => _employeeService.Value;



    }
}
