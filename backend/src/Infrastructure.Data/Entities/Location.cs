using Infrastructure.Shared.Database.Abstracts;

namespace Infrastructure.Data.Entities;

public class Location : DbEntity<int>
{
    public int PersonId { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? Postcode { get; set; }

    public virtual Person Person { get; set; }
    public virtual Street Street { get; set; }
    public virtual Coordinate Coordinates { get; set; }
    public virtual Timezone Timezone { get; set; }
}