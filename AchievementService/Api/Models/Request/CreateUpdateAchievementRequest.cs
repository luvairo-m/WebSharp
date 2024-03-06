using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Api.Models.Request;

public record CreateUpdateAchievementRequest
{
    [XmlIgnore]
    [JsonIgnore]
    public Guid Id { get; init; }

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