using Infrastructure.Data.Database;
using Infrastructure.Data.Extensions;
using Infrastructure.Http.Interfaces;
using Infrastructure.Shared.Common.Exceptions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Random.User.Domain.Queries.User.Get;

public class GetUserQueryHandler(DatabaseContext context, IRandomUserApi randomUserApi)
    : IRequestHandler<GetUserQueryRequest, GetUserQueryResponse>
{
    public async Task<GetUserQueryResponse> Handle(GetUserQueryRequest request, CancellationToken cancellationToken)
    {
        var person = await context.Persons
            .IncludeAll()
            .Include(x => x.Location)
            .ThenInclude(x => x.Coordinates)
            .Include(x => x.Location)
            .ThenInclude(x => x.Street)
            .Include(x => x.Location)
            .ThenInclude(x => x.Timezone)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (person == null) throw new NotFoundException(request.Id.ToString(), "Usuário não encontrado");

        return person.Adapt<GetUserQueryResponse>();
    }
}