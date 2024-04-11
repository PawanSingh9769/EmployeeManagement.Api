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
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        //logic for getting all the employees using company Id
        public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, bool trackChange) => await FindByCondition(e => e.CompanyId == companyId, trackChange).OrderBy(e =>e.Name).ToListAsync();
        
        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid Id , bool trackChanges) => await FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(Id), trackChanges).SingleOrDefaultAsync();

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee) => Delete(employee);

    }
}
