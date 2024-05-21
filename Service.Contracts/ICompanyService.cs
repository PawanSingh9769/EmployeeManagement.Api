
using Shared.DataTransferObject_DTO_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        //IEnumerable<Company> GetAllCompanies(bool trackChanges); removing the entity connection and connecting with DTO
       Task< IEnumerable<CompanyDto>> GetAllCompaniesAsync(bool trackChanges);

        Task<CompanyDto> GetCompanyAsync(Guid CompanyId, bool trackChanges);

        Task<CompanyDto> CreateCompanyAsync(CompanyForCreationDto Company);

       Task< IEnumerable<CompanyDto>>GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);


        //new method will accept the collection of companyForCreatingDto type as parameter 
        //return a tuple with two field as a result.
       Task< (IEnumerable<CompanyDto> companies, string ids)> CreateCompanyCollectionAsync(IEnumerable<CompanyForCreationDto> companyCollection);
        Task UpdateCompanyAsync(Guid companyid, CompanyForUpdateDto companyForUpdate,
bool trackChanges);
    }
}
