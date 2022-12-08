using Inflow.Modules.Customers.API;
using Inflow.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//register the modules
builder.Services.AddCustomersModule();
builder.Services.AddModularInfrastructure();

var app = builder.Build();

//initialize modules
app.UseCustomersModule();


app.UseAuthorization();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGet("/", context => context.Response.WriteAsync("Hello!"));
});

app.Run();
