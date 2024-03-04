using Dal.Repositories.AchievementRepository;
using Dal.Repositories.UserRepository;
using Dal.RepositoryManagers;
using Logic.Services.AchievementService;
using Logic.Services.UserService;

namespace Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUserAndAchievementLogicServices(this IServiceCollection collection)
    {
        collection.AddScoped<IAchievementService, AchievementService>();
        collection.AddScoped<IUserService, UserService>();

        return collection;
    }

    public static IServiceCollection AddUserAndAchievementRepositoriesWithManager(this IServiceCollection collection)
    {
        collection.AddScoped<IAchievementRepository, AchievementRepository>();
        collection.AddScoped<IUserRepository, UserRepository>();
        collection.AddScoped<IRepositoryManager, RepositoryManager>();

        return collection;
    }
}