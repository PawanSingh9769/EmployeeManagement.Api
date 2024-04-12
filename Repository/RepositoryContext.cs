using Entites.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;

namespace Repository
{
    //our class now inherits from the IdentityDbContext class and not
    //DbContext because we want to integrate our context with Identity.
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);   //This is required for migration to work properly
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());     //This line tells EF Core to apply the configuration specified 
                                                                            //in the CompanyConfiguration class to the corresponding entity (Company) 
                                                                           //in your DbContext
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }

        DbSet<Company> Companys { get; set; }
        DbSet<Employee> Employees { get; set; }
    }
}