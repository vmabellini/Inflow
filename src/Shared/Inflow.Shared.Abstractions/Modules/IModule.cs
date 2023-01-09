using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Shared.Abstractions.Modules
{
    /// <summary>
    /// Module abstraction interface. Follows the common pattern to have a method to configure services and one to use them.
    /// </summary>
    public interface IModule
    {
        string Name { get; }

        void Register(IServiceCollection services);
        void Use(IApplicationBuilder app);
    }
}
