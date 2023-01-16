using Inflow.Shared.Abstractions.Modules;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Shared.Infrastructure.Modules
{
    internal sealed class ModuleSubscriber : IModuleSubscriber
    {
        private readonly IModuleRegistry _moduleRegistry;
        private readonly IServiceProvider _serviceProvider;

        public ModuleSubscriber(IModuleRegistry moduleRegistry, IServiceProvider serviceProvider)
        {
            _moduleRegistry = moduleRegistry;
            _serviceProvider = serviceProvider;
        }

        public IModuleSubscriber Subscribe<TRequest, TResponse>(string path, Func<TRequest, IServiceProvider, CancellationToken, Task<TResponse>> action)
            where TRequest : class
            where TResponse : class
        {
            var registration = new ModuleRequestRegistration(typeof(TRequest), typeof(TResponse),
                async (request, CancellationToken) =>
                {
                    using var scope = _serviceProvider.CreateScope();
                    return await action((TRequest)request, scope.ServiceProvider, CancellationToken);
                });

            _moduleRegistry.AddRequestAction(path, registration);

            return this;
        }
    }
}
