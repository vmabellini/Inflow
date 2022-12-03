using Inflow.Modules.Customers.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Inflow.Bootstrapper")]
namespace Inflow.Modules.Customers.API
{
    internal static class Extensions
    {
        public static IServiceCollection AddCustomersModule(this IServiceCollection services)
        {
            services.AddCore();

            return services;
        }

        public static IApplicationBuilder UseCustomersModule(this IApplicationBuilder app)
        {
            app.UseCore();

            return app;
        }
    }
}
