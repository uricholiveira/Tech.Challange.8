using MediatR;

namespace Random.User.Domain.Commands.User.Delete;

public class DeleteUserCommandRequest : IRequest<DeleteUserCommandResponse>
{
    public required int Id { get; set; }
}