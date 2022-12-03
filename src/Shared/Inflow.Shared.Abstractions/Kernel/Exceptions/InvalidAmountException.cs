using Inflow.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Shared.Abstractions.Kernel.Exceptions
{
    public class InvalidAmountException : InflowException
    {
        public decimal Amount { get; }

        public InvalidAmountException(decimal amount) : base($"Amount: '{amount}' is invalid.")
        {
            Amount = amount;
        }
    }
}
