using System.Web;
using Infrastructure.Http.Interfaces;
using Infrastructure.Http.Models.RandomUser.GetUsers;
using Infrastructure.Shared.Common.Extensions;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Http.Services;

public class RandomUserApi(HttpClient httpClient, ILogger<RandomUserApi> logger) : IRandomUserApi
{
    public async Task<RandomUserHttpResponse> GetUsersAsync(RandomUserHttpRequest query,
        CancellationToken cancellationToken)
    {
        var queryParams = HttpUtility.ParseQueryString(string.Empty);

        if (query.Gender.HasValue)
            queryParams["gender"] = query.Gender.Value.ToString().ToLower();

        if (query.Count.HasValue)
            queryParams["results"] = query.Count.Value.ToString();

        var url = $"/api?{queryParams}";
        var response = await httpClient.GetAsync(url, cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsJsonAsync<RandomUserHttpResponse>();
    }
}