using Inflow.Modules.Customers.Core.Exceptions;

namespace Inflow.Modules.Customers.Core.Domain.ValueObjects
{
    internal record Name 
    {
        public string Value { get; }

        public Name(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length is > 50 or < 3)
            {
                throw new InvalidNameException(value);
            }

            Value = value.Trim().ToLowerInvariant().Replace(" ", ".");
        }

        public static implicit operator Name(string value) => new Name(value);

        public static implicit operator string(Name value) => value.Value;
    }
}
