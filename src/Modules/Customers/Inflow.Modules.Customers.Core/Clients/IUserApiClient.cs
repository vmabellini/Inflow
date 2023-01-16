using Inflow.Modules.Customers.Core.Clients.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Modules.Customers.Core.Clients
{
    internal interface IUserApiClient
    {
        Task<UserDto> GetAsync(string email);
    }
}
