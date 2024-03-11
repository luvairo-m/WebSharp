using Shared.Models;

namespace Services.Abstractions;

public interface IExperienceService
{
    Task GiveExperienceToUserAsync(
        Guid userId,
        ExperienceDto experience,
        CancellationToken cancellationToken = default);
    Task InvalidateUserAsync(Guid userId);
}