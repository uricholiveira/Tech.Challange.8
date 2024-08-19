using Infrastructure.Http.Models.RandomUser.GetUsers;

namespace Infrastructure.Http.Interfaces;

public interface IRandomUserApi
{
    public Task<RandomUserHttpResponse> GetUsersAsync(RandomUserHttpRequest query, CancellationToken cancellationToken);
}