using AutoMapper;
using CompanyEmployees.ActionFilters;
using CompanyEmployees.Extensions;
using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
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
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddResponseCaching();
builder.Services.ConfigureHttpCacheHeaders();
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);
//Without this code, our API wouldn’t work, and wouldn’t know where to 
//route incoming requests. But now, our app will find all of the controllers 
builder.Services.AddControllers().AddApplicationPart(typeof(CompanyEmployees.Presentation.AssemblyReference).Assembly);
//inside of the Presentation project and configure them with the 
//framework.
builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.AddControllers();


// Build method builds the WebApplication and registers all the services added with IOC
var app = builder.Build();
var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);
if (app.Environment.IsProduction())
    app.UseHsts();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();

app.UseStaticFiles();


app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");
app.UseResponseCaching();
app.UseHttpCacheHeaders();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
