using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Shared.Abstractions.Modules
{
    public interface IModuleSubscriber
    {
        IModuleSubscriber Subscribe<TRequest, TResponse>(string path, Func<TRequest, IServiceProvider, CancellationToken, Task<TResponse>> action)
            where TRequest : class 
            where TResponse : class;
    }
}
