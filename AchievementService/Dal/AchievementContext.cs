using Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dal;

public sealed class AchievementContext : DbContext
{
    public DbSet<AchievementDal> Achievements { get; set; } = null!;
    public DbSet<UserDal> Users { get; set; } = null!;

    public AchievementContext(DbContextOptions<AchievementContext> options) : base(options)
    {
        Database.EnsureCreated();   
    }
}