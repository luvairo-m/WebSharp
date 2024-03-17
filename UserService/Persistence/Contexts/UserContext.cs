using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
    }

    public DbSet<UserDal> Users { get; init; } = null!;
    public DbSet<UserInfoDal> UsersInfo { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserContext).Assembly);
    }
}