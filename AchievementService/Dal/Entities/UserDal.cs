using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dal.Entities;

[Table("user")]
public class UserDal
{
    [Key]
    public Guid Id { get; init; }

    [Required]
    [StringLength(30)]
    public string? Name { get; init; }

    [Required]
    [StringLength(30)]
    public string? Login { get; init; }

    public ICollection<AchievementDal>? Achievements { get; init; }
}