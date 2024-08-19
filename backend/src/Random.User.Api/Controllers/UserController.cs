using Microsoft.AspNetCore.Mvc;
using Random.User.Application.Interfaces;
using Random.User.Domain.Commands.User.Create;
using Random.User.Domain.Commands.User.Delete;
using Random.User.Domain.Commands.User.Login;
using Random.User.Domain.Queries.User.Get;
using Random.User.Domain.Queries.User.List;

namespace Random.User.Api.Controllers;

/// <summary>
/// </summary>
/// <param name="logger"></param>
/// <param name="userApplication"></param>
[ApiController]
[Route("api/user")]
public class UserController(ILogger<UserController> logger, IUserApplication userApplication)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUsers([FromBody] CreateUserCommandRequest request,
        CancellationToken cancellationToken = default)
    {
        await userApplication.CreateUserAsync(request, cancellationToken);

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserCommandRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await userApplication.LoginUserAsync(request, cancellationToken);

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserById(int id, CancellationToken cancellationToken = default)
    {
        var request = new GetUserQueryRequest { Id = id };
        var response = await userApplication.GetUserByIdAsync(request, cancellationToken);

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUserById(int id, CancellationToken cancellationToken = default)
    {
        var request = new DeleteUserCommandRequest { Id = id };
        var response = await userApplication.DeleteUserByIdAsync(request, cancellationToken);

        return Ok(response);
    }

    [HttpGet("/api/users")]
    public async Task<IActionResult> GetAllUsers([FromQuery] ListUsersQueryRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await userApplication.ListUsersAsync(request, cancellationToken);

        return Ok(response);
    }
}