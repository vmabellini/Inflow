using Inflow.Shared.Abstractions.Commands;
using Inflow.Shared.Abstractions.Time;
using Inflow.Shared.Infrastructure.Commands;
using Inflow.Shared.Infrastructure.Time;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Inflow.Bootstrapper")]
namespace Inflow.Shared.Infrastructure
{
    internal static class Extensions
    {
        public static IServiceCollection AddModularInfrastructure(this IServiceCollection services)
        {
            services
                .AddCommands()
                .AddSingleton<IClock, UtcClock>();

            return services;
        }
    }
}
