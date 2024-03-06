using Dal.Repositories.AchievementRepository;
using Dal.Repositories.UserRepository;
using Dal.RepositoryManagers;
using Microsoft.Extensions.DependencyInjection;

namespace Dal.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUserAndAchievementRepositoriesWithManager(this IServiceCollection collection)
    {
        collection.AddScoped<IAchievementRepository, AchievementRepository>();
        collection.AddScoped<IUserRepository, UserRepository>();
        collection.AddScoped<IRepositoryManager, RepositoryManager>();

        return collection;
    }
}