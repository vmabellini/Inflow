using Inflow.Shared.Abstractions.Kernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Modules.Customers.Core.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public Email Email { get; private set; }
        public string Name { get; private set; }
        public string FullName { get; private set; }
        public string Address { get; private set; }
        public string Natiolatity { get; private set; }
        public string Identity { get; private set; }
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
