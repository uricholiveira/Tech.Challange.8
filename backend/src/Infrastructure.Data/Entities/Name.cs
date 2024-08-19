using Infrastructure.Shared.Database.Abstracts;

namespace Infrastructure.Data.Entities;

public class Name : DbEntity<int>
{
    public int PersonId { get; set; }
    public string? Title { get; set; }
    public string? First { get; set; }
    public string? Last { get; set; }
    public virtual Person Person { get; set; }
}