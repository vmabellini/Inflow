using Inflow.Modules.Customers.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Modules.Customers.Core.Domain.Repositories
{
    internal interface ICustomerRepository
    {
        Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default);
        Task<Customer> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddAsync(Customer customer, CancellationToken cancellationToken = default);
        Task UpdateAsync(Customer customer, CancellationToken cancellationToken = default);
    }
}
