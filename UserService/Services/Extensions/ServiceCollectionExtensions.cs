using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;

namespace Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUserAndExperienceServices(this IServiceCollection collection)
    {
        collection.AddScoped<IUserService, UserService>();
        collection.AddScoped<IExperienceService, ExperienceService>();
        return collection;
    }
}