using Random.User.Domain.Core.Enums;

namespace Infrastructure.Http.Models.RandomUser.GetUsers;

public class RandomUserHttpRequest
{
    public EGender? Gender { get; set; }
    public int? Count { get; set; }
}