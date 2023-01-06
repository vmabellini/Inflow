using Inflow.Modules.Customers.Core.Domain.ValueObjects;
using Inflow.Shared.Abstractions.Kernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Modules.Customers.Core.DTO
{
    internal class CustomerDTO
    {
        public Guid Id { get; set; }

        public string Email { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Nationatity { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
