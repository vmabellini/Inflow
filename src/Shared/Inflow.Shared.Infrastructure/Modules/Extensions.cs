using Inflow.Shared.Abstractions.Commands;
using Inflow.Shared.Abstractions.Events;
using Inflow.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        internal static IServiceCollection AddModuleRequests(this IServiceCollection services,
            IEnumerable<Assembly> assemblies)
        {
            services.AddModuleRegistry(assemblies);
            
            services.AddSingleton<IModuleSubscriber, ModuleSubscriber>();
            services.AddSingleton<IModuleClient, ModuleClient>();
            services.AddSingleton<IModuleSerializer, JsonModuleSerializer>();

            return services;
        }

        private static IServiceCollection AddModuleRegistry(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            var registry = new ModuleRegistry();
            var types = assemblies.SelectMany(x => x.GetTypes()).ToArray();

            var eventTypes = types
                .Where(x => x.IsClass && typeof(IEvent).IsAssignableFrom(x))
                .ToArray();

            var commandTypes = types
                .Where(x => x.IsClass && typeof(ICommand).IsAssignableFrom(x))
                .ToArray();

            services.AddSingleton<IModuleRegistry>(sp =>
            {
                var commandDispatcher = sp.GetRequiredService<ICommandDispatcher>();
                var eventDispatcher = sp.GetRequiredService<IEventDispatcher>();

                //scans all event types/command types and automatically creates a lambda that
                //calls the dispatchers for all of them, allowing subscribers in other modules
                //to receive the events/commands

                foreach (var type in eventTypes)
                {
                    var registration = new ModuleBroadcastRegistration(type, (@event, cancellationToken) =>
                        (Task) eventDispatcher.GetType().GetMethod(nameof(eventDispatcher.PublishAsync))
                            ?.MakeGenericMethod(type)
                            .Invoke(eventDispatcher, new[] { @event, cancellationToken }));

                    registry.AddBroadcastAction(registration);
                }

                foreach (var type in commandTypes)
                {
                    var registration = new ModuleBroadcastRegistration(type, (@event, cancellationToken) =>
                        (Task)eventDispatcher.GetType().GetMethod(nameof(commandDispatcher.SendAsync))
                            ?.MakeGenericMethod(type)
                            .Invoke(eventDispatcher, new[] { @event, cancellationToken }));

                    registry.AddBroadcastAction(registration);
                }

                return registry;
            });

            return services;
        }

        public static IModuleSubscriber UseModuleRequests(this IApplicationBuilder app)
            => app.ApplicationServices.GetRequiredService<IModuleSubscriber>();
    }
}
