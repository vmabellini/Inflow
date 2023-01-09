using Inflow.Modules.Customers.API;
using Inflow.Shared.Infrastructure;
using Inflow.Shared.Infrastructure.Modules;

var builder = WebApplication.CreateBuilder(args);

var assemblies = ModuleLoader.LoadAssemblies(builder.Configuration);
var modules = ModuleLoader.LoadModules(assemblies);

//register the modules
builder.Services.AddModularInfrastructure(assemblies);

foreach (var module in modules)
{
    module.Register(builder.Services);
}

var app = builder.Build();

//initialize modules
foreach (var module in modules)
{
    module.Use(app);
}

app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGet("/", context => context.Response.WriteAsync("Hello!"));
});

app.Run();
