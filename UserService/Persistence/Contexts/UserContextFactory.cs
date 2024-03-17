using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts;

public class UserContextFactory : IDesignTimeDbContextFactory<UserContext>
{
    private const string appSettings = "appsettings.json";

    public UserContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "Web"))
            .AddUserSecrets<UserContextFactory>()
            .AddJsonFile(appSettings)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<UserContext>();
        var connection = configuration.GetConnectionString("Npgsql");

        optionsBuilder.UseNpgsql(connection + configuration["DbPassword"],
            contextOptions => contextOptions.MigrationsAssembly("Web"));

        return new UserContext(optionsBuilder.Options);
    }
}