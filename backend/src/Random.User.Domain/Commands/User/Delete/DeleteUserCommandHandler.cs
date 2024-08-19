using Infrastructure.Data.Database;
using Infrastructure.Http.Interfaces;
using Infrastructure.Shared.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Random.User.Domain.Commands.User.Delete;

public class DeleteUserCommandHandler(DatabaseContext context, IRandomUserApi randomUserApi)
    : IRequestHandler<DeleteUserCommandRequest, DeleteUserCommandResponse>
{
    public async Task<DeleteUserCommandResponse> Handle(DeleteUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        var person = await context.Persons
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (person == null) throw new NotFoundException(request.Id.ToString(), "Usuário não encontrado");

        if (person.Id == 1)
            throw new BadRequestException(person.Id.ToString(), "Usuário de demonstração não pode ser excluído");

        context.Remove(person);
        await context.SaveChangesAsync(cancellationToken);

        return new DeleteUserCommandResponse();
    }
}