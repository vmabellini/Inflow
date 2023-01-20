using Inflow.Shared.Abstractions.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Shared.Infrastructure.Modules
{
    internal class ModuleClient : IModuleClient
    {
        private readonly IModuleRegistry _moduleRegistry;
        private readonly IModuleSerializer _moduleSerializer;

        public ModuleClient(IModuleRegistry moduleRegistry,
            IModuleSerializer moduleSerializer)
        {
            _moduleRegistry = moduleRegistry;
            _moduleSerializer = moduleSerializer;
        }

        public async Task PublishAsync(object message, CancellationToken cancellationToken = default)
        {
            var key = message.GetType().Name;
            var registrations = _moduleRegistry
                .GetBroadcastRegistrations(key)
                .Where(x => x.ReceiverType != message.GetType());

            var tasks = new List<Task>();
            foreach (var registration in registrations)
            {
                var receiverMessage = TranslateType(message, registration.ReceiverType);
                tasks.Add(registration.Action(receiverMessage, cancellationToken));
            }
            await Task.WhenAll(tasks);
        }

        public async Task<TResult> SendAsync<TResult>(string path, object request, CancellationToken cancellationToken = default) where TResult : class
        {
            var registration = _moduleRegistry.GetRequestRegistration(path);
            if (registration is null)
                throw new InvalidOperationException($"No action defined for path {path}");

            var receiverRequest = TranslateType(request, registration.RequestType);

            var result = await registration.Action(receiverRequest, cancellationToken);

            return result is null ? null : TranslateType<TResult>(result);
        }

        /// <summary>
        /// Maps an object into another object serializing and deserializing it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        private T TranslateType<T>(object value)
            => _moduleSerializer.Deserialize<T>(_moduleSerializer.Serialize(value));

        /// <summary>
        /// Maps an object into another object serializing and deserializing it
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private object TranslateType(object value, Type type)
            => _moduleSerializer.Deserialize(_moduleSerializer.Serialize(value), type);
    }
}
