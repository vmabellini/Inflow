using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Shared.Infrastructure.Modules
{
    public class ModuleBroadcastRegistration
    {
        public Type ReceiverType { get; }
        public Func<object, CancellationToken, Task> Action { get; }
        public string Key => ReceiverType.Name;

        public ModuleBroadcastRegistration(Type receiverType, Func<object, CancellationToken, Task> action)
        {
            ReceiverType = receiverType;
            Action = action;
        }
    }
}
