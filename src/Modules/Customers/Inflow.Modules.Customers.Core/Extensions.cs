using Inflow.Modules.Customers.Core.Clients;
using Inflow.Modules.Customers.Core.DAL;
using Inflow.Modules.Customers.Core.DAL.Repositories;
using Inflow.Modules.Customers.Core.Domain.Repositories;
using Inflow.Shared.Infrastructure.Postgres;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Inflow.Modules.Customers.API")]
namespace Inflow.Modules.Customers.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddSingleton<IUserApiClient, UserApiClient>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddPostgres<CustomersDbContext>();

            return services;
        }

        public static IApplicationBuilder UseCore(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
