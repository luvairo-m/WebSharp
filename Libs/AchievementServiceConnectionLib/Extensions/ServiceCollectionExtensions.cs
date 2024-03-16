using AchievementServiceConnectionLib.Contracts;
using AchievementServiceConnectionLib.Services;
using HttpLogic.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace AchievementServiceConnectionLib.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAchievementConnectionLogic(this IServiceCollection collection)
    {
        collection.AddHttpLogic();
        collection.AddScoped<IAchievementConnectionService, AchievementConnectionService>();

        return collection;
    }
}