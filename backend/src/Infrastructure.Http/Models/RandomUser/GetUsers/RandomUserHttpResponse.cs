using System.Text.Json.Serialization;
using Infrastructure.Http.Converters;

namespace Infrastructure.Http.Models.RandomUser.GetUsers;

public class RandomUserHttpResponse
{
    public IEnumerable<RandomUserPersonHttpResponse> Results { get; set; }
}

public class RandomUserPersonHttpResponse
{
    public string? Gender { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Cell { get; set; }
    public string? Nat { get; set; }

    public RandomUserNameHttpResponse Name { get; set; }
    public RandomUserLocationHttpResponse Location { get; set; }
    public RandomUserLoginHttpResponse Login { get; set; }
    public RandomUserDobHttpResponse Dob { get; set; }
    public RandomUserRegisteredHttpResponse Registered { get; set; }
    [JsonPropertyName("id")] public RandomUserIdHttpResponse IdInfo { get; set; }
    public RandomUserPictureHttpResponse Picture { get; set; }
}

public class RandomUserNameHttpResponse
{
    public string? Title { get; set; }
    public string? First { get; set; }
    public string? Last { get; set; }
}

public class RandomUserLocationHttpResponse
{
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }

    [JsonConverter(typeof(PostcodeConverter))]
    public string? Postcode { get; set; }

    public RandomUserStreetHttpResponse Street { get; set; }
    public RandomUserCoordinatesHttpResponse Coordinates { get; set; }
    public RandomUserTimezoneHttpResponse Timezone { get; set; }
}

public class RandomUserStreetHttpResponse
{
    public int? Number { get; set; }
    public string? Name { get; set; }
}

public class RandomUserCoordinatesHttpResponse
{
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
}

public class RandomUserTimezoneHttpResponse
{
    public string? Offset { get; set; }
    public string? Description { get; set; }
}

public class RandomUserLoginHttpResponse
{
    public Guid Uuid { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Salt { get; set; }
    public string? Md5 { get; set; }
    public string? Sha1 { get; set; }
    public string? Sha256 { get; set; }
}

public class RandomUserDobHttpResponse
{
    public DateTime Date { get; set; }
    public int? Age { get; set; }
}

public class RandomUserRegisteredHttpResponse
{
    public DateTime? Date { get; set; }
    public int? Age { get; set; }
}

public class RandomUserIdHttpResponse
{
    public string? Name { get; set; }
    public string? Value { get; set; }
}

public class RandomUserPictureHttpResponse
{
    public string? Large { get; set; }
    public string? Medium { get; set; }
    public string? Thumbnail { get; set; }
}