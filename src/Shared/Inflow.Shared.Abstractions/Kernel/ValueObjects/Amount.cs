using Inflow.Shared.Abstractions.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Shared.Abstractions.Kernel.ValueObjects
{
    public record Amount
    {
        public decimal Value { get; }

        public Amount(decimal value)
        {
            if (value is < 0 or > 1000000)
            {
                throw new InvalidAmountException(value);
            }

            Value = value;
        }

        public static Amount Zero => new Amount(0m);

        public static implicit operator Amount(decimal value) => new(value);

        public static implicit operator decimal(Amount value) => value.Value;

        public static bool operator >(Amount a, Amount b) => a.Value > b.Value;

        public static bool operator <(Amount a, Amount b) => a.Value < b.Value;

        public static bool operator >=(Amount a, Amount b) => a.Value >= b.Value;

        public static bool operator <=(Amount a, Amount b) => a.Value <= b.Value;

        public static Amount operator +(Amount a, Amount b) => a.Value + b.Value;

        public static Amount operator -(Amount a, Amount b) => a.Value - b.Value;
    }
}
