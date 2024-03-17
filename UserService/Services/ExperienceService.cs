using Domain.Exceptions;
using Domain.Repository;
using Services.Abstractions;
using Shared.Models;

namespace Services;

internal class ExperienceService : IExperienceService
{
    private const int pointsPerLevel = 300;
    private readonly IRepositoryManager repositoryManager;

    public ExperienceService(IRepositoryManager repositoryManager)
    {
        this.repositoryManager = repositoryManager;
    }

    public async Task GiveExperienceToUserAsync(
        Guid userId,
        ExperienceDto experience,
        CancellationToken cancellationToken = default)
    {
        var user = await repositoryManager.Users.GetUserByIdWithoutTrackingAsync(userId, cancellationToken)
                   ?? throw new UserNotFoundException(userId);

        var totalLevel = Math.Max(0, user.Level + experience.LevelGain);
        var totalPoints = Math.Max(0, user.CurrentPoints + experience.PointsGain);

        totalLevel += totalPoints / pointsPerLevel;
        totalPoints %= pointsPerLevel;

        var updatedUser = user with
        {
            CurrentPoints = totalPoints,
            Level = totalLevel
        };

        repositoryManager.Users.UpdateUser(updatedUser);
        await repositoryManager.SaveChangesAsync(cancellationToken);
    }

    // Точка расширения для задания с Http-взаимодействием.
    public Task InvalidateUserAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}