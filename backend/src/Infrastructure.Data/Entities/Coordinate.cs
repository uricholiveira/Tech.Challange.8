using Infrastructure.Shared.Database.Abstracts;

namespace Infrastructure.Data.Entities;

public class Coordinate : DbEntity<int>
{
    public int LocationId { get; set; }
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }

    public virtual Location Location { get; set; }
}