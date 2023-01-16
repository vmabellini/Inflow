using Inflow.Shared.Abstractions.Commands;
using Inflow.Shared.Abstractions.Dispatchers;
using Inflow.Shared.Abstractions.Storage;
using Inflow.Shared.Abstractions.Time;
using Inflow.Shared.Infrastructure.Api;
using Inflow.Shared.Infrastructure.Auth;
using Inflow.Shared.Infrastructure.Commands;
using Inflow.Shared.Infrastructure.Dispatchers;
using Inflow.Shared.Infrastructure.Modules;
using Inflow.Shared.Infrastructure.Postgres;
using Inflow.Shared.Infrastructure.Queries;
using Inflow.Shared.Infrastructure.Storage;
using Inflow.Shared.Infrastructure.Time;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
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
    public static class Extensions
    {
        public static IServiceCollection AddModularInfrastructure(this IServiceCollection services, IList<System.Reflection.Assembly> assemblies)
        {
            var disabledModules = new List<string>();
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                foreach (var (key, value) in configuration.AsEnumerable())
                {
                    if (!key.Contains(":module:enabled"))
                    {
                        continue;
                    }

                    if (!bool.Parse(value))
                    {
                        disabledModules.Add(key.Split(":")[0]);
                    }
                }
            }

            services
                .AddMemoryCache()
                .AddHttpClient()
                .AddSingleton<IRequestStorage, RequestStorage>()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddAuth()
                .AddCommands(assemblies)
                .AddQueries(assemblies)
                .AddSingleton<IDispatcher, InMemoryDispatcher>()
                .AddPostgresOptions()
                .AddModuleRequests()
                .AddSingleton<IClock, UtcClock>()
                .AddControllers()
                .ConfigureApplicationPartManager(manager =>
                {
                    var removedParts = new List<ApplicationPart>();
                    foreach (var disabledModule in disabledModules)
                    {
                        var parts = manager.ApplicationParts.Where(x => x.Name.Contains(disabledModule, StringComparison.InvariantCultureIgnoreCase));
                        removedParts.AddRange(parts);
                    }

                    foreach (var removedPart in removedParts)
                    {
                        manager.ApplicationParts.Remove(removedPart);
                    }

                    manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
                });

            return services;
        }

        public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : class, new()
        {
            using var scope = services.BuildServiceProvider().CreateScope();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            return configuration.GetOptions<T>(sectionName);
        }

        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
        {
            var options = new T();
            configuration.GetSection(sectionName).Bind(options);
            return options;
        }
    }
}
