using Infrastructure.Shared.Database.Models;
using Infrastructure.Shared.Ioc.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Random.User.Application.Interfaces;
using Random.User.Domain.Commands.User.Create;
using Random.User.Domain.Commands.User.Delete;
using Random.User.Domain.Commands.User.Login;
using Random.User.Domain.Queries.User.Get;
using Random.User.Domain.Queries.User.List;

namespace Random.User.Application.Services;

public class UserApplication(ILogger<UserApplication> logger, ISender sender) : IUserApplication, IInjectScoped
{
    public async Task<LoginUserCommandResponse> LoginUserAsync(LoginUserCommandRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await sender.Send(request, cancellationToken);
        return response;
    }

    public async Task<CreateUserCommandResponse> CreateUserAsync(CreateUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(request, cancellationToken);
        return response;
    }

    public async Task<GetUserQueryResponse> GetUserByIdAsync(GetUserQueryRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await sender.Send(request, cancellationToken);
        return response;
    }

    public async Task<DeleteUserCommandResponse> DeleteUserByIdAsync(DeleteUserCommandRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await sender.Send(request, cancellationToken);
        return response;
    }

    public async Task<PagedList<ListUsersQueryResponse>> ListUsersAsync(ListUsersQueryRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await sender.Send(request, cancellationToken);
        return response;
    }
}