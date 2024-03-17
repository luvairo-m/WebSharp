namespace Domain.Models;

public record UserInfoDal
{
    public int Id { get; set; }

    public string? Email { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? About { get; init; }
    public DateTime? BirthDate { get; init; }

    // Relationship properties:
    public Guid UserId { get; init; }
    public UserDal? User { get; init; }
}