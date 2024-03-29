﻿using Inflow.Shared.Abstractions.Kernel.Exceptions;

namespace Inflow.Shared.Abstractions.Kernel.ValueObjects
{
    public record Currency
    {
        private static readonly HashSet<string> AllowedValues = new()
        {
            "PLN", "EUR", "GBP"
        };

        public string Value { get; }

        public Currency(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != 3)
            {
                throw new InvalidCurrencyException(value);
            }

            value = value.ToUpperInvariant();
            if (!AllowedValues.Contains(value))
            {
                throw new UnsupportedCurrencyException(value);
            }

            Value = value;
        }

        public static implicit operator Currency(string value) => new(value);

        public static implicit operator string(Currency value) => value.Value;
    }
}
