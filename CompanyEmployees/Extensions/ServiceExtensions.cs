using Contracts;
using LoggerService;
using Microsoft.AspNetCore.Mvc.Controllers;
using Repository;

namespace CompanyEmployees.Extensions
{
    public static class ServiceExtensions
    {
        //Extension method for CORs
        public static void ConfigureCores(this IServiceCollection services)  => services.AddCors(
            options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            }
            );

        //Extenion method for IISConfiguration 
        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(Options =>
            {

            });

        //Extension method for Logger
        public static void ConfigureLoggerService(this IServiceCollection services) =>
           services.AddSingleton<ILoggerManager, LoggerManager>();

        //Registering the IRepositoryManager And RepositoryManager class
        public static void ConfigureRepositoryManager(this IServiceCollection services) => services.AddScoped<IRepositoryManager , RepositoryManager>();
    }

}
