namespace Domain.Models;

public record UserDal
{
    public Guid Id { get; init; }

    public string? NickName { get; init; }
    public string? Country { get; init; }
    public string? AvatarUrl { get; init; }
    public int Level { get; init; }
    public int CurrentPoints { get; init; }

    // Relationship properties:
    public UserInfoDal? Info { get; init; }
}