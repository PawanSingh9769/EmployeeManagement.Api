using AutoMapper;
using Entites.Models;
using Shared.DataTransferObject_DTO_;

namespace CompanyEmployees
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            //createMap method it has source - company and destination - CompanyDTO to map
           // CreateMap<Company, CompanyDto>().DisableCtorValidation().ForCtorParam("fullAddress", opt => opt.MapFrom(x => string.Join("", x.Address, x.Country)));
            CreateMap<Company, CompanyDto>()
               .ConstructUsing((src, ctx) => new CompanyDto(src.Id, src.Name, $"{src.Address} {src.Country}"));


            //Mapping rule for Employee
            CreateMap<Employee, EmployeeDto>();

            //Mapping rule for Company creation
            CreateMap<CompanyForCreationDto, Company>();

            //Mapping rule for employee creation
            CreateMap<EmployeeForCreationDto, Employee>();
        }
    }
}
