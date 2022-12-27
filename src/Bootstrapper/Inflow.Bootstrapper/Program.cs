using Inflow.Modules.Customers.API;
using Inflow.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//register the modules
builder.Services.AddCustomersModule();
builder.Services.AddModularInfrastructure();

var app = builder.Build();

//initialize modules
app.UseCustomersModule();

app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGet("/", context => context.Response.WriteAsync("Hello!"));
});

app.Run();
