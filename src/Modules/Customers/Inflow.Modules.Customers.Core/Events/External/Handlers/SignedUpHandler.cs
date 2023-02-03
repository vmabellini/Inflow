using Inflow.Modules.Customers.Core.Domain.Entities;
using Inflow.Modules.Customers.Core.Domain.Repositories;
using Inflow.Modules.Customers.Core.Exceptions;
using Inflow.Shared.Abstractions.Events;
using Inflow.Shared.Abstractions.Messaging;
using Inflow.Shared.Abstractions.Time;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Inflow.Modules.Customers.Core.Events.External.Handlers
{
    internal class SignedUpHandler : IEventHandler<SignedUp>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IClock _clock;
        private readonly IMessageBroker _messageBroker;
        private readonly ILogger<SignedUpHandler> _logger;

        public SignedUpHandler(ICustomerRepository customerRepository,
            IClock clock,
            IMessageBroker messageBroker,
            ILogger<SignedUpHandler> logger)
        {
            _customerRepository = customerRepository;
            _clock = clock;
            _messageBroker = messageBroker;
            _logger = logger;
        }

        public async Task HandleAsync(SignedUp @event, CancellationToken cancellationToken = default)
        {
            if (@event.Role is not "user")
            {
                throw new UserNotInUserRoleException(@event.Role);
            }

            //reuse the same identifier
            var customerId = @event.UserId;
            if (await _customerRepository.GetAsync(customerId) is not null)
            {
                throw new CustomerAlreadyExistsException(customerId);
            }

            var customer = new Customer(customerId, @event.Email, _clock.CurrentDate());
            await _customerRepository.AddAsync(customer, cancellationToken);
            _logger.LogInformation($"Created customer Id {customer.Id}");
            await _messageBroker.PublishAsync(new CustomerCreated(customer.Id), cancellationToken);
        }
    }
}
