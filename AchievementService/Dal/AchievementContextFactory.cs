using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Dal;

public class AchievementContextFactory : IDesignTimeDbContextFactory<AchievementContext>
{
    private const string appSettings = "appsettings.json";

    public AchievementContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "Api"))
            .AddJsonFile(appSettings)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<AchievementContext>();
        optionsBuilder.UseSqlite(configuration.GetConnectionString("DefaultConnection"));

        return new AchievementContext(optionsBuilder.Options);
    }
}