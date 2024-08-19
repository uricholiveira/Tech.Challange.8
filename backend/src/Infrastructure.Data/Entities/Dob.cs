using Infrastructure.Shared.Database.Abstracts;

namespace Infrastructure.Data.Entities;

public class Dob : DbEntity<int>
{
    public int PersonId { get; set; }
    public DateTime? Date { get; set; }
    public int? Age { get; set; }
    public virtual Person Person { get; set; }
}