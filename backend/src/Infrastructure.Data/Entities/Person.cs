using Infrastructure.Shared.Database.Abstracts;

namespace Infrastructure.Data.Entities;

public class Person : DbEntity<int>
{
    public string? Gender { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Cell { get; set; }
    public string? Nat { get; set; }
    public virtual Name Name { get; set; }
    public virtual Location Location { get; set; }
    public virtual Login Login { get; set; }
    public virtual Dob Dob { get; set; }
    public virtual Registered Registered { get; set; }
    public virtual IdInfo IdInfo { get; set; }
    public virtual Picture Picture { get; set; }
}