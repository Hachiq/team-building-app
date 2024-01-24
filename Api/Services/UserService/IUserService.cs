using Api.Models;

namespace Api.Services.UserService
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserByUsernameAsync(string username);
        Task AddUserAsync(User user);
        Task<IEnumerable<string>> GetUserRolesAsync(int userId);
        Task AssignUserToRoleAsync(User user);
    }
}
