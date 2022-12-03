using Inflow.Modules.Customers.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//register the modules
builder.Services.AddCustomersModule();

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
