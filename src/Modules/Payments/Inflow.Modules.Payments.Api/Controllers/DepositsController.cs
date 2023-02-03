using System;
using System.Threading.Tasks;
using Inflow.Modules.Payments.Core.Deposits.Commands;
using Inflow.Modules.Payments.Core.Deposits.DTO;
using Inflow.Modules.Payments.Core.Deposits.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inflow.Shared.Abstractions.Dispatchers;
using Inflow.Shared.Abstractions.Queries;
using Inflow.Shared.Infrastructure.Api;

namespace Inflow.Modules.Payments.Api.Controllers;

[ApiController]
[Route("[controller]")]
internal class DepositsController : Controller
{
    private readonly IDispatcher _dispatcher;

    public DepositsController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPost]
    public async Task<ActionResult> Post(StartDeposit command)
    {
        await _dispatcher.SendAsync(command);
        return NoContent();
    }

    // Acting as a webhook for 3rd party payments service
    [HttpPut("{depositId:guid}/complete")]
    public async Task<ActionResult> Post(Guid depositId, CompleteDeposit command)
    {
        await _dispatcher.SendAsync(command.Bind(x => x.DepositId, depositId));
        return NoContent();
    }
}