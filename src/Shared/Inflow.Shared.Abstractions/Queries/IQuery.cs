using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Shared.Abstractions.Queries
{
    public interface IQuery { }
    public interface IQuery<T> : IQuery { }
}
