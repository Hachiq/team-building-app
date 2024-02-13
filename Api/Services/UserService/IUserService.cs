using Api.DTOs;
using Api.Models;

namespace Api.Services.UserService
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync();
        Task<List<User>> GetUsersByTeamIdAsync(int id);
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByRefreshTokenAsync(string refreshToken);
        Task UpdateUserRefreshToken(User user, RefreshToken refreshToken);
        Task ExpireRefreshToken(User user);
        Task AddUserAsync(User user);
        Task<IEnumerable<string>> GetUserRolesAsync(int userId);
        Task AssignUserToUserRoleAsync(User user);
        Task AssignUserToLeaderRoleAsync(User user);
        Task UpdateUserCredentials(User user, UserCredentialsDto creds);
    }
}
