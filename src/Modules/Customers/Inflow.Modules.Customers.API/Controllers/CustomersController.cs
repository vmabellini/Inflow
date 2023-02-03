using Inflow.Modules.Customers.Core.Commands;
using Inflow.Modules.Customers.Core.DTO;
using Inflow.Modules.Customers.Core.Queries;
using Inflow.Shared.Abstractions.Commands;
using Inflow.Shared.Abstractions.Dispatchers;
using Inflow.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Modules.Customers.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class CustomersController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public CustomersController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("{customerId:guid}")]
        public async Task<ActionResult<CustomerDetailsDTO>> Get(Guid customerId)
        {
            var response = await _dispatcher.QueryAsync(new GetCustomer() { Id = customerId });
            if (response is null)
                return NotFound();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Post(CreateCustomer command)
        {
            await _dispatcher.SendAsync(command);
            return NoContent();
        }

        [HttpPut("complete")]
        public async Task<ActionResult> Post(CompleteCustomer command)
        {
            await _dispatcher.SendAsync(command);
            return NoContent();
        }

        [HttpPut("{customerId:guid}/verify")]
        public async Task<ActionResult> Post(Guid customerId, VerifyCustomer command)
        {
            await _dispatcher.SendAsync(command.Bind(x => x.CustomerId, customerId));
            return NoContent();
        }

        [HttpPut("{customerId:guid}/lock")]
        public async Task<ActionResult> Post(Guid customerId, LockCustomer command)
        {
            await _dispatcher.SendAsync(command.Bind(x => x.CustomerId, customerId));
            return NoContent();
        }

        [HttpPut("{customerId:guid}/unlock")]
        public async Task<ActionResult> Post(Guid customerId, UnlockCustomer command)
        {
            await _dispatcher.SendAsync(command.Bind(x => x.CustomerId, customerId));
            return NoContent();
        }
    }
}
