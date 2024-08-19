using MediatR;

namespace Random.User.Domain.Commands.User.Login;

public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
{
    public string Username { get; set; }
    public string Password { get; set; }
}