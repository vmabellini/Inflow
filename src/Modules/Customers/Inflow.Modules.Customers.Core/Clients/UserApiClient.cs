using Inflow.Modules.Customers.Core.Clients.DTO;
using Inflow.Shared.Abstractions.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Modules.Customers.Core.Clients
{
    internal sealed class UserApiClient : IUserApiClient
    {
        private readonly IModuleClient _moduleClient;

        public UserApiClient(IModuleClient moduleClient)
        {
            _moduleClient = moduleClient;
        }

        public async Task<UserDto> GetAsync(string email)
            => await _moduleClient.SendAsync<UserDto>("users/get", new { email });
    }
}
