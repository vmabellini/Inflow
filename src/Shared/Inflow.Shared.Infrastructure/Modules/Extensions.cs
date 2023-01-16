using Inflow.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Shared.Infrastructure.Modules
{
    public static class Extensions
    {
        internal static WebApplicationBuilder ConfigureModules(this WebApplicationBuilder builder)
        {
            builder.Host.ConfigureAppConfiguration((ctx, cfg) =>
            {
                foreach (var settings in GetSettings("*"))
                {
                    cfg.AddJsonFile(settings);
                }

                IEnumerable<string> GetSettings(string pattern)
                    => Directory.EnumerateFiles(ctx.HostingEnvironment.ContentRootPath, $"module.{pattern}.json", SearchOption.AllDirectories);
            });

            return builder;
        }

        internal static IServiceCollection AddModuleRequests(this IServiceCollection services)
        {
            services.AddSingleton<IModuleRegistry, ModuleRegistry>();
            services.AddSingleton<IModuleSubscriber, ModuleSubscriber>();
            services.AddSingleton<IModuleClient, ModuleClient>();
            services.AddSingleton<IModuleSerializer, JsonModuleSerializer>();

            return services;
        }

        public static IModuleSubscriber UseModuleRequests(this IApplicationBuilder app)
            => app.ApplicationServices.GetRequiredService<IModuleSubscriber>();
    }
}
