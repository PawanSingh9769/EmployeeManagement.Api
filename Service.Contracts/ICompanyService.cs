
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
        IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
    }
}
