using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class IdInfoDbConfig : IEntityTypeConfiguration<IdInfo>
{
    public void Configure(EntityTypeBuilder<IdInfo> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.ToTable("id_infos");
    }
}