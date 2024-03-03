using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Api.Models.Request;

public record CreateAchievementRequest
{
    [Required]
    [MaxLength(30)]
    public string? Title { get; init; }

    [DefaultValue("Description not provided.")]
    [MaxLength(100)]
    public string? Description { get; init; }

    [MaxLength(100)]
    public string? ImageUrl { get; init; }

    [Required]
    [Range(1, 100)]
    public int? Points { get; init; }
}