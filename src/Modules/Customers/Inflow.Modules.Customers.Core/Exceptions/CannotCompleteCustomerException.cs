using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Customers.Core.Exceptions
{
    internal class CannotCompleteCustomerException : InflowException
    {
        public CannotCompleteCustomerException(Guid customerId)
            : base($"Customer Id {customerId} cannot be completed")
        {

        }
    }
}
