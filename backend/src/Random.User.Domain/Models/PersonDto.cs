namespace Random.User.Domain.Models;

public class PersonDto
{
    public int Id { get; set; }
    public string? Gender { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Cell { get; set; }
    public string? Nat { get; set; }
    public NameDto Name { get; set; }
    public LocationDto Location { get; set; }
    public LoginDto Login { get; set; }
    public DobDto Dob { get; set; }
    public RegisteredDto Registered { get; set; }
    public IdInfoDto IdInfo { get; set; }
    public PictureDto Picture { get; set; }
}