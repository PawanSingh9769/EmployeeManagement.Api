using CompanyEmployees.Extensions;

var builder = WebApplication.CreateBuilder(args);     // this line creates builder variable of type webApplicationBuilder

// Add services to the container.
builder.Services.ConfigureCores();
builder.Services.ConfigureIISIntegration();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
