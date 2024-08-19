using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class TimezoneDbConfig : IEntityTypeConfiguration<Timezone>
{
    public void Configure(EntityTypeBuilder<Timezone> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.ToTable("timezones");
    }
}