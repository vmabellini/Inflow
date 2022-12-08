using Inflow.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Modules.Customers.Core.Exceptions
{
    internal class CustomerNotActiveException : InflowException
    {
        public CustomerNotActiveException(Guid customerId) 
            : base($"Customer Id {customerId} not active")
        {

        }
    }
}
