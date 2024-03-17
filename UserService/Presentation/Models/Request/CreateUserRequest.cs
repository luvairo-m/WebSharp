using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.Request;

public record CreateUserRequest
{
    [Required]
    [StringLength(20)]
    public string? NickName { get; init; }

    [Required]
    [EmailAddress]
    public string? Email { get; init; }

    [StringLength(20)]
    public string? Country { get; init; }

    [StringLength(100)]
    public string? AvatarUrl { get; init; }

    [StringLength(15)]
    public string? FirstName { get; init; }

    [StringLength(20)]
    public string? LastName { get; init; }

    [StringLength(75)]
    public string? About { get; init; }

    public DateTime? BirthDate { get; init; }
}