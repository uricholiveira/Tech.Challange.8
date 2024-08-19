using Infrastructure.Shared.Database.Abstracts;

namespace Infrastructure.Data.Entities;

public class Timezone : DbEntity<int>
{
    public int LocationId { get; set; }
    public string? Offset { get; set; }
    public string? Description { get; set; }

    public virtual Location Location { get; set; }
}