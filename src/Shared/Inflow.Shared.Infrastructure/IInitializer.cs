using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Shared.Infrastructure
{
    public interface IInitializer
    {
        Task InitAsync();
    }
}
