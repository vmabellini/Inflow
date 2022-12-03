using Inflow.Shared.Abstractions.Kernel.Exceptions;

namespace Inflow.Shared.Abstractions.Kernel.ValueObjects
{
    public record FullName
    {
        public string Value { get; }

        public FullName(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length is > 100 or < 2)
            {
                throw new InvalidFullNameException(value);
            }

            Value = value;
        }

        public static implicit operator FullName(string value) => new FullName(value);

        public static implicit operator string(FullName value) => value.Value;
    }
}
