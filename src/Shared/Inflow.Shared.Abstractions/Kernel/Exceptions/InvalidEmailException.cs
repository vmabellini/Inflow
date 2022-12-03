using Inflow.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Shared.Abstractions.Kernel.Exceptions
{
    public class InvalidEmailException : InflowException
    {
        public InvalidEmailException(string email) : base($"Email {email} is invalid")
        {
            Email = email;
        }

        public string Email { get; }
    }
}
