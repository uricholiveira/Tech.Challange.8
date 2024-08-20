using Infrastructure.Data.Entities;

namespace Random.User.Domain.Queries.User.List;

public class ListUsersQueryResponse
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public DateTime CreatedAt { get; set; }
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