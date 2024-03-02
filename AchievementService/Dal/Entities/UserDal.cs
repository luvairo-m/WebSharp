using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities;

namespace Dal.Entities;

[Table("user")]
public record UserDal : BaseEntity<Guid>
{
    [Required]
    [StringLength(30)]
    public string? Name { get; init; }

    [Required]
    [StringLength(30)]
    public string? Login { get; init; }

    public ICollection<AchievementDal>? Achievements { get; init; }
}