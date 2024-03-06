using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public record BaseEntity<T>
{
    [Required]
    public T? Id { get; init; }
}