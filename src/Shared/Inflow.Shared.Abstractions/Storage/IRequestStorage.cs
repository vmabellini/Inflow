using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Shared.Abstractions.Storage
{
    public interface IRequestStorage
    {
        void Set<T>(string key, T value, TimeSpan? duration = null);
        T Get<T>(string key);
    }
}
