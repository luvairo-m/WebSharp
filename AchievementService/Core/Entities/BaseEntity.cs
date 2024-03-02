namespace Core.Entities;

public record BaseEntity<T>
{
    public T Id { get; init; }
}