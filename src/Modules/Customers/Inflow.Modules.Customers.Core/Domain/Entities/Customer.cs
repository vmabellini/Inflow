﻿using Inflow.Modules.Customers.Core.Domain.ValueObjects;
using Inflow.Modules.Customers.Core.Exceptions;
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
        public Nationality Nationatity { get; private set; }
        public Identity Identity { get; private set; }
        public string Notes { get; private set; }
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

        public void Complete(Name name, FullName fullName, Address address, Nationality nationality, Identity identity, DateTime completedAt)
        {
            if (!IsActive)
            {
                throw new CustomerNotActiveException(Id);
            }

            if (CompletedAt.HasValue)
            {
                throw new CannotCompleteCustomerException(Id);
            }

            Name = name;
            FullName = fullName;
            Address = address;
            Nationatity = nationality;
            Identity = identity;
            CompletedAt = completedAt;
        }

        public void Verify(DateTime verifiedAt)
        {
            if (!IsActive)
            {
                throw new CustomerNotActiveException(Id);
            }

            if (!CompletedAt.HasValue || VerifiedAt.HasValue)
            {
                throw new CannotVerifyCustomerException(Id);
            }

            VerifiedAt = verifiedAt;
        }

        public void Lock(string notes = null)
        {
            IsActive = false;
            Notes = notes;
        }

        public void Unlock(string notes = null)
        {
            IsActive = true;
            Notes = notes;
        }
    }
}
