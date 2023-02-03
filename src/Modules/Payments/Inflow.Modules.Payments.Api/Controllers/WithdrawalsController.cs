using System.Threading.Tasks;
using Inflow.Modules.Payments.Core.Withdrawals.Commands;
using Inflow.Modules.Payments.Core.Withdrawals.DTO;
using Inflow.Modules.Payments.Core.Withdrawals.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inflow.Shared.Abstractions.Dispatchers;
using Inflow.Shared.Abstractions.Queries;
using Inflow.Shared.Infrastructure.Api;

namespace Inflow.Modules.Payments.Api.Controllers;

[ApiController]
[Route("[controller]")]
internal class WithdrawalsController : Controller
{
    private readonly IDispatcher _dispatcher;

    public WithdrawalsController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Post(StartWithdrawal command)
    {
        await _dispatcher.SendAsync(command);
        return NoContent();
    }
}