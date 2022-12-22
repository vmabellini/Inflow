using Inflow.Modules.Customers.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Modules.Customers.Core.DAL
{
    internal class CustomersDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public CustomersDbContext(DbContextOptions<CustomersDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("customers");
            //scans all configurations in this assembly
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly); 

            base.OnModelCreating(modelBuilder);
        }
    }
}
