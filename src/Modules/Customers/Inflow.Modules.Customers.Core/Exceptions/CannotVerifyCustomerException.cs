using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Customers.Core.Exceptions
{
    internal class CannotVerifyCustomerException : InflowException
    {
        public CannotVerifyCustomerException(Guid customerId)
            : base($"Customer Id {customerId} cannot be verified")
        {

        }
    }
}
