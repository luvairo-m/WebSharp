namespace Presentation.Models.Request;

public record UserExperienceRequest
{
    public int PointsGain { get; init; }
    public int LevelGain { get; init; }
}