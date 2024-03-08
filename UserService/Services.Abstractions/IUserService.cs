using Shared.Models;

namespace Services.Abstractions;

public interface IUserService
{
    Task<ICollection<UserDto>> GetAllUsersAsync(CancellationToken cancellationToken = default);
    Task<UserDto> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken);
    Task UpdateUserAsync(UserDto userDto);
    Task CreateUserAsync(UserDto userDto);
}