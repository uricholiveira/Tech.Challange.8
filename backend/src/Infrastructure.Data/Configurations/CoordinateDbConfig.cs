using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class CoordinateDbConfig : IEntityTypeConfiguration<Coordinate>
{
    public void Configure(EntityTypeBuilder<Coordinate> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.ToTable("coordinates");
    }
}