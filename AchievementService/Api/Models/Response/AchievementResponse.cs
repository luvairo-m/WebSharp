namespace Api.Models.Response;

public record AchievementResponse
{
    public Guid Id { get; init; }
    public string? Title { get; init; }
    public string? Description { get; init; }
    public string? ImageUrl { get; init; }
    public int? Points { get; init; }
}