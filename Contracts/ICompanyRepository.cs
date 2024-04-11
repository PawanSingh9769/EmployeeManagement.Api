using Entites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges); // getting all the companies list
       Task< Company> GetCompanyAsync(Guid ComapnyId, bool trackChanges);

        void CreateCompany(Company company);

       Task< IEnumerable<Company>> GetByidsAsync(IEnumerable<Guid > ids, bool trackChanges);

        

    }
}
