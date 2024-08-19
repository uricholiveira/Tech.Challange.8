using Infrastructure.Shared.Database.Models;
using Random.User.Domain.Commands.User.Create;
using Random.User.Domain.Commands.User.Delete;
using Random.User.Domain.Commands.User.Login;
using Random.User.Domain.Queries.User.Get;
using Random.User.Domain.Queries.User.List;

namespace Random.User.Application.Interfaces;

public interface IUserApplication
{
    public Task<LoginUserCommandResponse> LoginUserAsync(LoginUserCommandRequest request,
        CancellationToken cancellationToken = default);

    public Task<CreateUserCommandResponse> CreateUserAsync(CreateUserCommandRequest request,
        CancellationToken cancellationToken = default);

    public Task<GetUserQueryResponse> GetUserByIdAsync(GetUserQueryRequest request,
        CancellationToken cancellationToken = default);

    public Task<DeleteUserCommandResponse> DeleteUserByIdAsync(DeleteUserCommandRequest request,
        CancellationToken cancellationToken = default);

    public Task<PagedList<ListUsersQueryResponse>> ListUsersAsync(ListUsersQueryRequest request,
        CancellationToken cancellationToken = default);
}