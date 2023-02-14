using System;
using System.Threading.Tasks;
using Inflow.Modules.Wallets.Application.Wallets.DTO;
using Inflow.Modules.Wallets.Application.Wallets.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inflow.Shared.Abstractions.Dispatchers;
using Inflow.Shared.Abstractions.Queries;

namespace Inflow.Modules.Wallets.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
internal class WalletsController : Controller
{
    private const string Policy = "wallets";
    private readonly IDispatcher _dispatcher;

    public WalletsController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Paged<WalletDto>>> BrowseAsync([FromQuery] BrowseWallets query)
    {
        return Ok(await _dispatcher.QueryAsync(query));
    }

    [HttpGet("{walletId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<WalletDetailsDto>> GetAsync(Guid walletId)
    {
        var wallet = await _dispatcher.QueryAsync(new GetWallet { WalletId = walletId });
        if (wallet is not null)
        {
            return Ok(wallet);
        }

        return NotFound();
    }
}