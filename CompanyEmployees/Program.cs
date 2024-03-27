using CompanyEmployees.Extensions;
using Microsoft.AspNetCore.Routing;
using NLog;
using NLog.Config;

var builder = WebApplication.CreateBuilder(args);     // this line creates builder variable of type webApplicationBuilder

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));



// Add services to the container.
builder.Services.ConfigureCores();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSqlContext(builder.Configuration);

//Without this code, our API wouldn’t work, and wouldn’t know where to 
//route incoming requests. But now, our app will find all of the controllers 
builder.Services.AddControllers().AddApplicationPart(typeof(CompanyEmployees.Presentation.AssemblyReference).Assembly); 
//inside of the Presentation project and configure them with the 
//framework.

builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
