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
        Task<bool> ExistsAsync(string name);
        Task<Customer> GetAsync(Guid id);
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
    }
}
