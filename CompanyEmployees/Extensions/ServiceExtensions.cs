using Contracts;
using LoggerService;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service;
using Service.Contracts;
using System;

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

        //registering the IServiceManager and serviceManager class
        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();


        //, we have the RepositoryManager service 
        // registration, which happens at runtime, and during that registration, we
        // must have RepositoryContext registered as well in the runtime, so we
        //could inject it into other services(like RepositoryManager service).

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) => services.AddDbContext<RepositoryContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
    }

}
