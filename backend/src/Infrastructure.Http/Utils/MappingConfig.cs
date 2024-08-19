using Infrastructure.Data.Entities;
using Infrastructure.Http.Models.RandomUser.GetUsers;
using Mapster;

namespace Infrastructure.Http.Utils;

public class MappingConfig
{
    public static void RegisterHttpMappings()
    {
        TypeAdapterConfig<RandomUserLoginHttpResponse, Login>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Uuid);
    }
}