using Inflow.Modules.Customers.Core.DAL;
using Inflow.Modules.Customers.Core.DTO;
using Inflow.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Modules.Customers.Core.Queries.Handlers
{
    internal sealed class GetCustomerHandler : IQueryHandler<GetCustomer, CustomerDetailsDTO>
    {
        private readonly CustomersDbContext _dbContext;

        public GetCustomerHandler(CustomersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CustomerDetailsDTO> HandleAsync(GetCustomer query, CancellationToken token = default)
        {
            var customer = await _dbContext
                .Customers
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == query.Id, token);

            return customer?.AsDetailsDto();
        }
    }
}
