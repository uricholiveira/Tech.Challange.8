using System.Reflection;
using Infrastructure.Shared.Database.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Database;

public class NoDatabaseContext : DbContext
{
    public NoDatabaseContext(DbContextOptions options) : base(options)
    {
    }

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