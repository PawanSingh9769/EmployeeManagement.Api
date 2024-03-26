using CompanyEmployees.Extensions;
using NLog;
using NLog.Config;

var builder = WebApplication.CreateBuilder(args);     // this line creates builder variable of type webApplicationBuilder

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));



// Add services to the container.
builder.Services.ConfigureCores();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
