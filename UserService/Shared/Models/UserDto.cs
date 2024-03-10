using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Shared.Models;

public record UserDto
{
    [JsonIgnore]
    [XmlIgnore]
    public Guid Id { get; init; }

    public string? NickName { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? AvatarUrl { get; init; }
    public string? Country { get; init; }
    public DateTime BirthDate { get; init; }
    public int Level { get; init; }
    public int CurrentPoints { get; init; }
    public ICollection<AchievementDto>? Achievements { get; init; }
}