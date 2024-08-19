using Infrastructure.Shared.Database.Abstracts;

namespace Infrastructure.Data.Entities;

public class Street : DbEntity<int>
{
    public int LocationId { get; set; }
    public int? Number { get; set; }
    public string? Name { get; set; }

    public virtual Location Location { get; set; }
}