using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class PersonDbConfig : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasOne(p => p.Name)
            .WithOne(n => n.Person)
            .HasForeignKey<Name>(n => n.PersonId);

        builder
            .HasOne(p => p.Location)
            .WithOne(l => l.Person)
            .HasForeignKey<Location>(l => l.PersonId);

        builder
            .HasOne(p => p.Login)
            .WithOne(l => l.Person)
            .HasForeignKey<Login>(l => l.PersonId);

        builder
            .HasOne(p => p.Dob)
            .WithOne(d => d.Person)
            .HasForeignKey<Dob>(d => d.PersonId);

        builder
            .HasOne(p => p.Registered)
            .WithOne(r => r.Person)
            .HasForeignKey<Registered>(r => r.PersonId);

        builder
            .HasOne(p => p.IdInfo)
            .WithOne(i => i.Person)
            .HasForeignKey<IdInfo>(i => i.PersonId);

        builder
            .HasOne(p => p.Picture)
            .WithOne(p => p.Person)
            .HasForeignKey<Picture>(p => p.PersonId);

        builder.ToTable("persons");
    }

    // private void SeedData(EntityTypeBuilder<Person> builder)
    // {
    //     // Caminho do arquivo JSON
    //     var filePath = Path.Combine(AppContext.BaseDirectory, "Seeders", "person.json");
    //
    //     // Lê o arquivo JSON
    //     var jsonData = File.ReadAllText(filePath);
    //
    //     // Deserializa o JSON em uma lista de objetos Person
    //     var personsDeserialized = JsonSerializer.Deserialize<object[]>(jsonData);
    //     var person = JsonConvert.DeserializeObject<Person>(personsDeserialized[0].ToString());
    //
    //     // Configura o seed data utilizando o Mapster
    //     if (person == null) return;
    //     builder.HasData(person);
    // }
}