using Contracts;
using Entites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CompanyRepository : RepositoryBase<Company> , ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        //logic to extract all the comapnies list
        public IEnumerable<Company> GetAllCompanies(bool trackChanges) => FindAll(trackChanges).OrderBy(c => c.Name).ToList();




        //logic to get the company using ID

        public Company GetCompany(Guid ComapnyId, bool trackChanges) => FindByCondition(c => c.Id == ComapnyId, trackChanges).SingleOrDefault();

       
    }
}
