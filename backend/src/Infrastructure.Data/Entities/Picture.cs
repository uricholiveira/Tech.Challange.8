using Infrastructure.Shared.Database.Abstracts;

namespace Infrastructure.Data.Entities;

public class Picture : DbEntity<int>
{
    public int PersonId { get; set; }
    public string? Large { get; set; }
    public string? Medium { get; set; }
    public string? Thumbnail { get; set; }

    public virtual Person Person { get; set; }
}