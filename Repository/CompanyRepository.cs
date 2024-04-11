using Contracts;
using Entites.Models;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges) => await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();




        //logic to get the company using ID

        public async Task<Company> GetCompanyAsync(Guid ComapnyId, bool trackChanges) => await FindByCondition(c => c.Id == ComapnyId, trackChanges).SingleOrDefaultAsync();

        //logic to create company
        public void CreateCompany(Company company) => Create(company);

        //logic to get companybyids?????
        public async Task<IEnumerable<Company>> GetByidsAsync(IEnumerable<Guid> ids, bool trackChanges) => await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();

        

       
    }
}
