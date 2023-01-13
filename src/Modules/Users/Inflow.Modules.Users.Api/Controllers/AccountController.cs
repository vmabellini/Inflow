using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inflow.Modules.Users.Core.Commands;
using Inflow.Modules.Users.Core.DTO;
using Inflow.Modules.Users.Core.Queries;
using Inflow.Modules.Users.Core.Services;
using Inflow.Shared.Abstractions.Dispatchers;

namespace Inflow.Modules.Users.Api.Controllers;

internal class AccountController : BaseController
{
    private const string AccessTokenCookie = "__access-token";
    private readonly IDispatcher _dispatcher;
    private readonly IUserRequestStorage _userRequestStorage;
    private readonly CookieOptions _cookieOptions;

    public AccountController(IDispatcher dispatcher, IUserRequestStorage userRequestStorage,
        CookieOptions cookieOptions)
    {
        _dispatcher = dispatcher;
        _userRequestStorage = userRequestStorage;
        _cookieOptions = cookieOptions;
    }

    [HttpPost("sign-up")]
    public async Task<ActionResult> SignUpAsync(SignUp command)
    {
        await _dispatcher.SendAsync(command);
        return NoContent();
    }

    [HttpPost("sign-in")]
    public async Task<ActionResult<UserDetailsDto>> SignInAsync(SignIn command)
    {
        await _dispatcher.SendAsync(command);
        var jwt = _userRequestStorage.GetToken(command.Id);
        var user = await _dispatcher.QueryAsync(new GetUser {UserId = jwt.UserId});
        AddCookie(AccessTokenCookie, jwt.AccessToken);
        return Ok(user);
    }

    private void AddCookie(string key, string value) => Response.Cookies.Append(key, value, _cookieOptions);

    private void DeleteCookie(string key) => Response.Cookies.Delete(key, _cookieOptions);
}