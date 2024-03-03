using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities;

namespace Dal.Entities;

[Table("Users")]
public record UserDal : BaseEntity<Guid>
{
    [ForeignKey("AchievementId")]
    public ICollection<AchievementDal> Achievements { get; init; }
}