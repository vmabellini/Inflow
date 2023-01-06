using Inflow.Modules.Customers.Core.DTO;
using Inflow.Shared.Abstractions.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Modules.Customers.Core.Queries
{
    internal class GetCustomer : IQuery<CustomerDetailsDTO>
    {
        public Guid Id { get; set; }
    }
}
