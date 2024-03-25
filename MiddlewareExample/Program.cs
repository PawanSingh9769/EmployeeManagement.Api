var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.Use(async (context, next) =>
{
    Console.WriteLine($"Logic before executing the next delegate in Use Method");
    await next.Invoke();
    Console.WriteLine($"Logic after executing the next delagrte in Use method");
});
app.Run(async context =>
{
    Console.WriteLine($"Writing response to client  in the run method");
    await context.Response.WriteAsync("Hello from Middleware component. ");
});

app.Map("/usingmapbranch", builder =>
{
    builder.Use(async (context, next) =>
    {
        Console.WriteLine($"Map Logic before executing the next delegate in Use Method");
        await next.Invoke();
        Console.WriteLine($"Logic after executing the next delagrte in Use method");
    });

    builder.Run(async context =>
    {
        Console.WriteLine($"Writing response to client  in the run method");
        await context.Response.WriteAsync("Hii from Middleware component. ");
    });
});
app.MapControllers();

app.Run();
