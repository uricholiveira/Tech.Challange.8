using Infrastructure.Shared.Database.Abstracts;

namespace Infrastructure.Data.Entities;

public class IdInfo : DbEntity<int>
{
    public int PersonId { get; set; }
    public string? Name { get; set; }
    public string? Value { get; set; }

    public virtual Person Person { get; set; }
}