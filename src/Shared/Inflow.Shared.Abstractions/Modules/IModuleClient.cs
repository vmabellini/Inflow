using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Shared.Abstractions.Modules
{
    public interface IModuleClient
    {
        Task<TResult> SendAsync<TResult>(string path, object request, CancellationToken cancellationToken = default)
            where TResult : class;

        Task PublishAsync(object message, CancellationToken cancellationToken = default);
    }
}
