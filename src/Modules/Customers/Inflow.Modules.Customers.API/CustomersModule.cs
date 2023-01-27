using Inflow.Modules.Customers.Core;
using Inflow.Modules.Customers.Core.Events.External;
using Inflow.Shared.Abstractions.Modules;
using Inflow.Shared.Infrastructure.Contracts;
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
    internal class CustomersModule : IModule
    {
        public string Name => "Customers";

        public void Register(IServiceCollection services)
        {
            services.AddCore();
        }

        public void Use(IApplicationBuilder app)
        {
            app.UseCore();

            app.UseContracts()
                .Register<SignedUpContract>();
        }
    }
}
