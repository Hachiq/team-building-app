using Api.Models;

namespace Api.Services.UserService
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByRefreshTokenAsync(string refreshToken);
        Task UpdateUserRefreshToken(User user, RefreshToken refreshToken);
        Task AddUserAsync(User user);
        Task<IEnumerable<string>> GetUserRolesAsync(int userId);
        Task AssignUserToRoleAsync(User user);
    }
}
