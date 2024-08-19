using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Infrastructure.Data.Database;
using Infrastructure.Http.Interfaces;
using Infrastructure.Shared.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Random.User.Domain.Commands.User.Login;

public class LoginUserCommandHandler(DatabaseContext context, IRandomUserApi randomUserApi, IConfiguration config)
    : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
{
    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request,
        CancellationToken cancellationToken)
    {
        var user = await context.Logins
            .Include(x => x.Person)
            .FirstOrDefaultAsync(l => l.Username == request.Username, cancellationToken);

        if (user == null) throw new NotFoundException(request.Username, "Usuário não encontrado");

        if (user.Password != request.Password) throw new BadRequestException("Senha incorreta");

        var token = GenerateJwtToken(user);

        return new LoginUserCommandResponse
        {
            AccessToken = token
        };
    }

    // TODO: Se sobrar tempo, passar essa função pra Infrastructure.Shared.Authentication
    private string GenerateJwtToken(Infrastructure.Data.Entities.Login user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Person.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Authentication:Jwt:SecurityKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            config["Authentication:Jwt:Issuer"],
            config["Authentication:Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}