using MediatR;
using Random.User.Domain.Core.Enums;

namespace Random.User.Domain.Commands.User.Create;

public class CreateUserCommandRequest : IRequest<CreateUserCommandResponse>
{
    public EGender? Gender { get; set; }
    public int? Count { get; set; }
}