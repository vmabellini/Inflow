using Inflow.Modules.Customers.Core.Domain.ValueObjects;
using Inflow.Shared.Abstractions.Kernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Modules.Customers.Core.Domain.Entities
{
    internal class Customer
    {
        public Guid Id { get; private set; }

        public Email Email { get; private set; }
        public Name Name { get; private set; }
        public FullName FullName { get; private set; }
        public Address Address { get; private set; }
        public Nationality Natiolatity { get; private set; }
        public Identity Identity { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public DateTime? VerifiedAt { get; private set; }

        private Customer()
        {
        }

        public Customer(Guid id, Email email, DateTime createdAt)
        {
            Id = id;
            Email = email;
            CreatedAt = createdAt;
        }
    }
}
