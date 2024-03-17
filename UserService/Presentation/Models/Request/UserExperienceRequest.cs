using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.Request;

public record UserExperienceRequest
{
    [Required]
    public int? PointsGain { get; init; }
    
    [Required]
    public int? LevelGain { get; init; }
}