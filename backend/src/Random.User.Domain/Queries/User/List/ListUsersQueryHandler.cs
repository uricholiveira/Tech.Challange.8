using Infrastructure.Data.Database;
using Infrastructure.Data.Entities;
using Infrastructure.Data.Extensions;
using Infrastructure.Http.Interfaces;
using Infrastructure.Shared.Database.Extensions;
using Infrastructure.Shared.Database.Models;
using MediatR;

namespace Random.User.Domain.Queries.User.List;

public class ListUsersQueryHandler(DatabaseContext context, IRandomUserApi randomUserApi)
    : IRequestHandler<ListUsersQueryRequest, PagedList<ListUsersQueryResponse>>
{
    public async Task<PagedList<ListUsersQueryResponse>> Handle(ListUsersQueryRequest request,
        CancellationToken cancellationToken)
    {
        var query = context.Persons.IncludeAll().AsQueryable();

        if (request.Gender.HasValue) query = query.Where(x => x.Gender == request.Gender.Value.ToString().ToLower());

        if (!string.IsNullOrEmpty(request.FirstName))
            query = query.Where(x => request.FirstName.ToLower().Contains(x.Name.First.ToLower()));

        return await query.ToPagedListAsync<Person, ListUsersQueryResponse>(request.Page, request.PageSize,
            cancellationToken);
    }
}