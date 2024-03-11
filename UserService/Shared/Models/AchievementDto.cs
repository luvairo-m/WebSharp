namespace Shared.Models;

// Модель для Http-взаимодействия с AchievementService.
public record AchievementDto
{
    public string? Title { get; init; }
    public string? Description { get; init; }
    public string? ImageUrl { get; init; }
    public int Points { get; init; }
}