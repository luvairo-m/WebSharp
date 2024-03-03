using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities;

namespace Dal.Entities;

[Table("Achievements")]
public record AchievementDal : BaseEntity<Guid>
{
    [Required]
    [StringLength(30)]
    public string? Title { get; init; }

    [StringLength(100)]
    [DefaultValue("No description.")]
    public string? Description { get; init; }

    [StringLength(100)]
    public string? ImageUrl { get; init; }

    [Range(1, 100)]
    public int Points { get; init; }

    [Required]
    [ForeignKey("UserId")]
    public List<UserDal>? Users { get; init; }
}