using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repository;

namespace CompanyEmployees.ContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
             var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) //This line tells the configuration builder to look for configuration files (like appsettings.json) in the current directory.
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<RepositoryContext>().UseSqlServer(configuration.GetConnectionString("sqlConnection"),
                b => b.MigrationsAssembly("CompanyEmployees")); //(telling EF core where the migration is) 
                                                               //migration assembly is not in our main project, so we changed it for migration assembly

            return new RepositoryContext(builder.Options);
        }
    }
}
