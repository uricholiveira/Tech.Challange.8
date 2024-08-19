using Infrastructure.Shared.Database.Abstracts;

namespace Infrastructure.Data.Entities;

public class Login : DbEntity<Guid>
{
    public int PersonId { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Salt { get; set; }
    public string? Md5 { get; set; }
    public string? Sha1 { get; set; }
    public string? Sha256 { get; set; }

    public virtual Person Person { get; set; }
}