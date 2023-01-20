using Inflow.Shared.Abstractions.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Inflow.Shared.Infrastructure.Events
{
    internal static class Extensions
    {
        public static IServiceCollection AddEvents(this IServiceCollection services, IList<System.Reflection.Assembly> assemblies)
        {
            services
                .AddSingleton<IEventDispatcher, EventDispatcher>()
                .Scan(x => x.FromAssemblies(assemblies)
                    .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            return services;
        }
    }
}
