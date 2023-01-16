using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Modules.Customers.Core.Clients.DTO
{
    internal class UserDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
