using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Customers.Core.Exceptions
{
    internal class CustomerAlreadyExistsException : InflowException
    {
        public CustomerAlreadyExistsException(Guid customerId):base($"The customer with id {customerId} already exists")
        {

        }
    }
}
