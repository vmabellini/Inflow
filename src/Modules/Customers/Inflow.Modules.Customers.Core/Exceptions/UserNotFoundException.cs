using Inflow.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Modules.Customers.Core.Exceptions
{
    internal class UserNotFoundException : InflowException
    {
        public UserNotFoundException(string email) : base($"User with email {email} wasn't found")
        {

        }
    }
}
