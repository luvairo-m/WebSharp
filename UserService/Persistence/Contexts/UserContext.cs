using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<UserDal> Users { get; set; } = null!;
    public DbSet<UserInfoDal> UsersInfo { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserContext).Assembly);
    }
}