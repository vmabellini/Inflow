using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Shared.Abstractions.Exceptions
{
    public abstract class InflowException : Exception
    {
        protected InflowException(string message) : base(message)
        {

        }
    }
}
