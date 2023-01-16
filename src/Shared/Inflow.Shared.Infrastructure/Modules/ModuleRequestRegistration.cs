﻿namespace Inflow.Shared.Infrastructure.Modules
{
    internal class ModuleRequestRegistration
    {
        public ModuleRequestRegistration(Type requestType,
            Type responseType,
            Func<object, CancellationToken, Task<object>> action)
        {
            RequestType = requestType;
            ResponseType = responseType;
            Action = action;
        }

        public Type RequestType { get; }
        public Type ResponseType { get; }
        public Func<object, CancellationToken, Task<object>> Action { get; }
    }
}
