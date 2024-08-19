using System.Reflection;
using Infrastructure.Data.Entities;
using Infrastructure.Shared.Database.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Name> Names { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Street> Streets { get; set; }
    public DbSet<Coordinate> Coordinates { get; set; }
    public DbSet<Timezone> Timezones { get; set; }
    public DbSet<Login> Logins { get; set; }
    public DbSet<Dob> Dobs { get; set; }
    public DbSet<Registered> Registers { get; set; }
    public DbSet<IdInfo> IdInfos { get; set; }
    public DbSet<Picture> Pictures { get; set; }
    public DbSet<Person> Persons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        SetUpdateAndCreatedDateTimeOnChangedDbEntities<Guid>();
        SetUpdateAndCreatedDateTimeOnChangedDbEntities<int>();

        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        SetUpdateAndCreatedDateTimeOnChangedDbEntities<Guid>();
        SetUpdateAndCreatedDateTimeOnChangedDbEntities<int>();

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void SetUpdateAndCreatedDateTimeOnChangedDbEntities<T>()
    {
        var entries = ChangeTracker.Entries<DbEntity<T>>()
            .Where(e => e.State is EntityState.Added or EntityState.Modified);

        foreach (var entityEntries in entries)
            switch (entityEntries.State)
            {
                case EntityState.Added:
                    entityEntries.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entityEntries.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
    }
}