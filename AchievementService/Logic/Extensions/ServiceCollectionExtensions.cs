using Logic.Services.AchievementService;
using Logic.Services.UserService;
using Microsoft.Extensions.DependencyInjection;

namespace Logic.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUserAndAchievementLogicServices(this IServiceCollection collection)
    {
        collection.AddScoped<IAchievementService, AchievementService>();
        collection.AddScoped<IUserService, UserService>();

        return collection;
    }
}