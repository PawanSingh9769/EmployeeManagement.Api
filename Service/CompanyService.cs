﻿using AutoMapper;
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
    /*****************************************************************************************************
     * 
     *  
     * 
     * The CompanyService class implements the ICompanyService interface and handles business logic specific to company-related operations.
     * It may use a Mapper to convert between entities and DTOs.
     * 
     * 
     *****************************************************************************************************/

    internal sealed class CompanyService : ICompanyService // inheriting the IService class 
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public CompanyService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper) //injecting IRepository and Ilogger interface
        {
            _repository = repository; // to access the repository method from each user (companyrepo or Employee repo)
            _logger = logger; // to access logging method 
            _mapper = mapper;
        }

        

        /* We are using our repository manager to call the GetAllCompanies
          method from the CompanyRepository class and return all the companies
          from the database.*/
        public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
        {
            //Manually mapping 
            //var companiesDto = companies.Select(c =>  
            // new CompanyDto(c.Id, c.Name ?? "", string.Join(' ',
            // c.Address, c.Country))) .ToList();

            //Automatic mapping using auto mapper
            var companies = _repository.Company.GetAllCompanies(trackChanges);
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return companiesDto;

        }

        public CompanyDto GetCompany(Guid CompanyId, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(CompanyId, trackChanges);
            
            if(company == null)
            {
                throw new CompanyNotFoundException(CompanyId);
            }
            

            var companyDto = _mapper.Map<CompanyDto>(company);
            return companyDto;
        }


        public CompanyDto CreateCompany(CompanyForCreationDto company)
        {
            /*
             *        Here, we map the company for creation to the company entity, call the 
                      repository method for creation, and call the Save() method to save the 
                      entity to the database. After that, we map the company entity to the 
                      company DTO object to return it to the controller. 
             * 
             */
            var companyEntity = _mapper.Map<Company>(company);
            _repository.Company.CreateCompany(companyEntity);
            _repository.Save();
            var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);
            return companyToReturn;

        }
    }
}
