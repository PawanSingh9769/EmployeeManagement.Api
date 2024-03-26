using Entites.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;

namespace Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfiguration(new CompanyConfiguration()); //This line tells EF Core to apply the configuration specified 
                                                                       //in the CompanyConfiguration class to the corresponding entity (Company) 
                                                                       //in your DbContext
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        }

        DbSet<Company> Companys { get; set; }
        DbSet<Employee> Employees { get; set; }
    }
}