using Infrastructure.Shared.Database.Models;
using MediatR;
using Random.User.Domain.Core.Enums;

namespace Random.User.Domain.Queries.User.List;

public class ListUsersQueryRequest : IRequest<PagedList<ListUsersQueryResponse>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public EGender? Gender { get; set; }
    public string? FirstName { get; set; }
}