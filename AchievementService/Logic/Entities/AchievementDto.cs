namespace Logic.Entities;

public record AchievementDto(Guid Id, string Title, string Description, string? ImageUrl, int Points);