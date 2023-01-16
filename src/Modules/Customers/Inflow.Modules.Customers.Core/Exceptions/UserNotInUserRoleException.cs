using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Customers.Core.Exceptions
{
    internal class UserNotInUserRoleException : InflowException
    {
        public UserNotInUserRoleException(string role):base($"User hasn't a regular user role")
        {

        }
    }
}
