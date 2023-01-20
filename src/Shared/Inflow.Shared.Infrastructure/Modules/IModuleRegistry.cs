using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Shared.Infrastructure.Modules
{
    internal interface IModuleRegistry
    {
        ModuleRequestRegistration GetRequestRegistration(string path);
        void AddRequestAction(string path, ModuleRequestRegistration registration);

        IEnumerable<ModuleBroadcastRegistration> GetBroadcastRegistrations(string key);
        void AddBroadcastAction(ModuleBroadcastRegistration registration);
    }
}
