﻿using Inflow.Modules.Customers.Core.Commands;
using Inflow.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Modules.Customers.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public CustomersController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        public async Task<IActionResult> Post(CreateCustomer command)
        {
            await _commandDispatcher.SendAsync(command);
            return NoContent();
        }
    }
}
