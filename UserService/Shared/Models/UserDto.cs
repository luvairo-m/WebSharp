namespace Shared.Models;

public record UserDto
{
    public Guid Id { get; init; }

    public string? NickName { get; init; }
    public string? Country { get; init; }
    public string? AvatarUrl { get; init; }
    public string? Email { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? About { get; init; }
    public DateTime BirthDate { get; init; }
    public int Level { get; init; }
    public int CurrentPoints { get; init; }
    public IList<AchievementDto> Achievements { get; init; } = new List<AchievementDto>();
}