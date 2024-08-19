using Infrastructure.Data.Entities;

namespace Random.User.Domain.Queries.User.List;

public class ListUsersQueryResponse : Person
{
    public string? Email { get; set; }
    public ListUsersNameQueryResponse Name { get; set; }
    public ListUsersLoginQueryResponse Login { get; set; }
    public ListUserPictureQueryResponse Picture { get; set; }
}

public class ListUsersNameQueryResponse
{
    public string? First { get; set; }
    public string? Last { get; set; }
}

public class ListUsersLoginQueryResponse
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}

public class ListUserPictureQueryResponse
{
    public string? Thumbnail { get; set; }
}