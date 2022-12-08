using Inflow.Modules.Customers.Core.Domain.Entities;
using Inflow.Modules.Customers.Core.Domain.Repositories;
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
        private readonly IClock _clock;
        private readonly ILogger<CreateCustomerHandler> _logger;

        public CreateCustomerHandler(ICustomerRepository customerRepository,
            IClock clock,
            ILogger<CreateCustomerHandler> logger)
        {
            _customerRepository = customerRepository;
            _clock = clock;
            _logger = logger;
        }

        public async Task HandleAsync(CreateCustomer command, CancellationToken cancellationToken = default)
        {
            //validate
            _ = new Email(command.Email);

            var customer = new Customer(Guid.NewGuid(), command.Email, _clock.CurrentDate());
            await _customerRepository.AddAsync(customer, cancellationToken);
            _logger.LogInformation($"Created customer Id {customer.Id}");
        }
    }
}
