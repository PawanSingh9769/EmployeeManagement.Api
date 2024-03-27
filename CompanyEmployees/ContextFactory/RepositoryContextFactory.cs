using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CompanyEmployees.ContextFactory
{
      //this class is used only for configureing database connection and logic 
     // RepositoryContextFactory class, which implements the 
    // IDesignTimeDbContextFactory interface, we have registered our
    //RepositoryContext class at design time.This helps us find the
    //RepositoryContext class in another project while executing migrations.
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
