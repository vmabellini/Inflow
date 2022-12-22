using Inflow.Modules.Customers.Core.Domain.Entities;
using Inflow.Modules.Customers.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Modules.Customers.Core.DAL.Repositories
{
    internal class CustomerRepository : ICustomerRepository
    {
        private readonly CustomersDbContext _context;
        private readonly DbSet<Customer> _customers;

        public CustomerRepository(CustomersDbContext context)
        {
            _context = context;
            _customers = context.Customers;
        }

        public async Task AddAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            await _customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default)
            => _customers.AnyAsync(x => x.Name == name);

        public Task<Customer> GetAsync(Guid id, CancellationToken cancellationToken = default)
            => _customers.SingleOrDefaultAsync(x => x.Id == id);

        public async Task UpdateAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            _customers.Update(customer);
            await _context.SaveChangesAsync();
        }
    }
}
