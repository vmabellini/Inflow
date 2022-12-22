using Inflow.Modules.Customers.Core.Domain.Entities;
using Inflow.Modules.Customers.Core.Domain.ValueObjects;
using Inflow.Shared.Abstractions.Kernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Modules.Customers.Core.DAL.Configurations
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasIndex(x => x.Email).IsUnique(); //guarantees the uniqueness
            builder.Property(x => x.Email)
                .HasMaxLength(100)
                .HasConversion(x => x.Value, x => new Email(x)); //converts a value object to a string and vice-versa

            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .HasConversion(x => x.Value, x => new Name(x));

            builder.Property(x => x.FullName)
                .HasMaxLength(100)
                .HasConversion(x => x.Value, x => new FullName(x));

            builder.Property(x => x.Address)
               .HasMaxLength(200)
               .HasConversion(x => x.Value, x => new Address(x));

            builder.Property(x => x.Identity)
               .HasMaxLength(40)
               .HasConversion(x => x.ToString(), x => Identity.From(x));

            builder.Property(x => x.Nationatity)
               .HasMaxLength(2)
               .HasConversion(x => x.Value, x => new Nationality(x));

            builder.Property(x => x.Notes)
               .HasMaxLength(500);
        }
    }
}
