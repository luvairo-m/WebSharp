using Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dal;

public sealed class AchievementContext : DbContext
{
    public AchievementContext(DbContextOptions<AchievementContext> options) : base(options)
    {
    }

    public DbSet<AchievementDal> Achievements { get; init; } = null!;
    public DbSet<UserDal> Users { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<UserDal>()
            .HasMany(user => user.Achievements)
            .WithMany(achievement => achievement.Users)
            .UsingEntity(builder => builder.ToTable("UserAchievement"));
    }
}