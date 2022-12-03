using Inflow.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Modules.Customers.Core.Exceptions
{
    internal class InvalidNameException : InflowException
    {
        public string Name { get; }

        public InvalidNameException(string name) : base($"Name: '{name}' is invalid.")
        {
            Name = name;
        }
    }
}
