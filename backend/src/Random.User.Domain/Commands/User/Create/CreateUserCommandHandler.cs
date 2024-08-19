using Infrastructure.Data.Database;
using Infrastructure.Data.Entities;
using Infrastructure.Http.Interfaces;
using Infrastructure.Http.Models.RandomUser.GetUsers;
using Mapster;
using MediatR;

namespace Random.User.Domain.Commands.User.Create;

public class CreateUserCommandHandler(DatabaseContext context, IRandomUserApi randomUserApi)
    : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
{
    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        var response = await randomUserApi.GetUsersAsync(request.Adapt<RandomUserHttpRequest>(), cancellationToken);

        var persons = response.Results.Adapt<IEnumerable<Person>>();

        context.Persons.AddRange(persons);
        await context.SaveChangesAsync(cancellationToken);

        return new CreateUserCommandResponse();
    }
}