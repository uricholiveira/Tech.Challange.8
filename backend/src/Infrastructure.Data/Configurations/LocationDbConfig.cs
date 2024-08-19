using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class LocationDbConfig : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(p => p.Street)
            .WithOne(s => s.Location)
            .HasForeignKey<Street>(s => s.LocationId);

        builder
            .HasOne(p => p.Coordinates)
            .WithOne(c => c.Location)
            .HasForeignKey<Coordinate>(c => c.LocationId);

        builder
            .HasOne(p => p.Timezone)
            .WithOne(t => t.Location)
            .HasForeignKey<Timezone>(t => t.LocationId);

        builder.ToTable("locations");
    }
}