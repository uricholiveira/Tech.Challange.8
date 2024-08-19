using Infrastructure.Data.Database;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data.Seeders;

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new DatabaseContext(
            serviceProvider.GetRequiredService<DbContextOptions<DatabaseContext>>());

        // Adicionando dados de semente
        if (context.Persons.Any()) return;

        context.Persons.Add(new Person
        {
            Gender = "male",
            Name = new Name
            {
                Title = "Mrs",
                First = "Demo",
                Last = "User"
            },
            Location = new Location
            {
                Street = new Street
                {
                    Number = 6294,
                    Name = "Porodice Praizović"
                },
                City = "Požega",
                State = "Rasina",
                Country = "Serbia",
                Postcode = "88493",
                Coordinates = new Coordinate
                {
                    Latitude = "61.4917",
                    Longitude = "84.4481"
                },
                Timezone = new Timezone
                {
                    Offset = "+3:30",
                    Description = "Tehran"
                }
            },
            Email = "demo@example.com",
            Login = new Login
            {
                Id = Guid.Parse("dfb9aaaa-d637-41d1-a443-3069966dbfb3"),
                Username = "demo",
                Password = "demo",
                Salt = "zmEe2gjs",
                Md5 = "5a86aebc030298d929f574bae2335b92",
                Sha1 = "09ab08236c7d0adf0c755c384b02ce209350e7ba",
                Sha256 = "9c814c4f32641f95f0999e23e3fc32eab81b0daf1e9c811ad770cd58cbfbc58f"
            },
            Dob = new Dob
            {
                Date = DateTime.Now.AddYears(-18).ToUniversalTime(),
                Age = 55
            },
            Registered = new Registered
            {
                Date = DateTime.Now.ToUniversalTime(),
                Age = 10
            },
            Phone = "024-8797-257",
            Cell = "068-4326-524",
            IdInfo = new IdInfo
            {
                Name = "SID",
                Value = "095041942"
            },
            Picture = new Picture
            {
                Large = "https://randomuser.me/api/portraits/women/56.jpg",
                Medium = "https://randomuser.me/api/portraits/med/women/56.jpg",
                Thumbnail = "https://randomuser.me/api/portraits/thumb/women/56.jpg"
            },
            Nat = "RS"
        });

        context.SaveChanges();
    }
}