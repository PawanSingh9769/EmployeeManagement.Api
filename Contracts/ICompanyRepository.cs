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
        IEnumerable<Company> GetAllCompanies(bool trackChanges); // getting all the companies list
        Company GetCompany(Guid ComapnyId, bool trackChanges);

        void CreateCompany(Company company);

        IEnumerable<Company> GetByids(IEnumerable<Guid > ids, bool trackChanges);

        

    }
}
