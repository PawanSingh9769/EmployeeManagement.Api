using Contracts;
using Entites.Models;
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
        public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChange) => FindByCondition(e => e.CompanyId == companyId, trackChange).OrderBy(e =>e.Name).ToList();
        
        public Employee GetEmployee(Guid companyId, Guid Id , bool trackChanges) => FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(Id), trackChanges).SingleOrDefault();

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee) => Delete(employee);

    }
}
