using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dal.Entities;

[Table("achievement")]
public class AchievementDal
{
    [Key]
    public int Id { get; init; }

    [Required]
    [StringLength(30)]
    public string? Title { get; init; }

    [DefaultValue(1)]
    [Range(1, 100)]
    public int Points { get; init; }

    [StringLength(100)]
    public string? Description { get; init; }
}