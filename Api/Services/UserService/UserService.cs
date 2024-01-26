using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;

        public UserService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }

        public async Task UpdateUserRefreshToken(User user, RefreshToken refreshToken)
        {
            user.RefreshToken = refreshToken.Token;
            user.TokenCreated = refreshToken.Created;
            user.TokenExpires = refreshToken.Expires;

            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }

        public async Task ExpireRefreshToken(User user)
        {
            user.TokenExpires = DateTime.Now;
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }

        public async Task AddUserAsync(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(int userId)
        {
            var user = await _db.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user?.UserRoles.Select(ur => ur.Role.Name) ?? Enumerable.Empty<string>();
        }

        public async Task AssignUserToRoleAsync(User user)
        {
            await _db.UserRoles.AddAsync(new UserRole
            {
                UserId = user.Id,
                RoleId = 3
            });
            await _db.SaveChangesAsync();
        }
    }
}
