using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    // RepositoryManager implements IRepositoryManager
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext; // DbContext for data access
        private readonly Lazy<ICompanyRepository> _companyRepository; // Lazy-loaded Company repository
        private readonly Lazy<IEmployeeRepository> _employeeRepository; // Lazy-loaded Employee repository

        // Constructor to initialize RepositoryManager with a RepositoryContext
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext; // Assign the provided RepositoryContext

            // Lazy initialization of CompanyRepository using a lambda expression
            _companyRepository = new Lazy<ICompanyRepository>(() => new CompanyRepository(repositoryContext));

            // Lazy initialization of EmployeeRepository using a lambda expression
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(_repositoryContext));
        }

        // Property to access the lazy-loaded Company repository
        public ICompanyRepository Company => _companyRepository.Value;

        // Property to access the lazy-loaded Employee repository
        public IEmployeeRepository Employee => _employeeRepository.Value;

       // IEmployeeRepository IRepositoryManager.Employee => throw new NotImplementedException();

        // Save method to persist changes to the database using the RepositoryContext
        public void Save() => _repositoryContext.SaveChanges();
    }
}
