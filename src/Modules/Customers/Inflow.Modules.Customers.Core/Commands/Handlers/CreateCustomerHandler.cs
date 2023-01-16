using Inflow.Modules.Customers.Core.Clients;
using Inflow.Modules.Customers.Core.Domain.Entities;
using Inflow.Modules.Customers.Core.Domain.Repositories;
using Inflow.Modules.Customers.Core.Exceptions;
using Inflow.Shared.Abstractions.Commands;
using Inflow.Shared.Abstractions.Kernel.ValueObjects;
using Inflow.Shared.Abstractions.Time;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Modules.Customers.Core.Commands.Handlers
{
    internal sealed class CreateCustomerHandler : ICommandHandler<CreateCustomer>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserApiClient _userApiClient;
        private readonly IClock _clock;
        private readonly ILogger<CreateCustomerHandler> _logger;

        public CreateCustomerHandler(ICustomerRepository customerRepository,
            IUserApiClient userApiClient,
            IClock clock,
            ILogger<CreateCustomerHandler> logger)
        {
            _customerRepository = customerRepository;
            _userApiClient = userApiClient;
            _clock = clock;
            _logger = logger;
        }

        public async Task HandleAsync(CreateCustomer command, CancellationToken cancellationToken = default)
        {
            //validate
            _ = new Email(command.Email);
            var user = await _userApiClient.GetAsync(command.Email);
            if (user is null)
            {
                throw new UserNotFoundException(command.Email);
            }

            if (user.Role is not "user")
            {
                throw new UserNotInUserRoleException(user.Role);
            }
            
            //reuse the same identifier
            var customerId = user.UserId;
            if (await _customerRepository.GetAsync(customerId) is not null)
            {
                throw new CustomerAlreadyExistsException(customerId);
            }

            var customer = new Customer(customerId, command.Email, _clock.CurrentDate());
            await _customerRepository.AddAsync(customer, cancellationToken);
            _logger.LogInformation($"Created customer Id {customer.Id}");
        }
    }
}
