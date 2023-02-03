﻿using Inflow.Modules.Payments.Core.Deposits.Domain.Entities;
using Inflow.Modules.Payments.Infrastructure.ValueObjects;
using Inflow.Shared.Abstractions.Kernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inflow.Modules.Payments.Core.DAL.Configurations;

internal class DepositAccountConfiguration : IEntityTypeConfiguration<DepositAccount>
{
    public void Configure(EntityTypeBuilder<DepositAccount> builder)
    {
        builder.HasIndex(x => new { x.CustomerId, x.Currency }).IsUnique();
            
        builder.Property(x => x.Currency).IsRequired()
            .HasConversion(x => x.Value, x => new Currency(x));
            
        builder.Property(x => x.Iban).IsRequired()
            .HasConversion(x => x.Value, x => new Iban(x));
    }
}