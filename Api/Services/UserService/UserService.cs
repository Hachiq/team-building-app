using Api.Data;
using Api.DTOs;
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

        public async Task<List<User>> GetUsersByTeamIdAsync(int id)
        {
            return await _db.Users.Where(u => u.Team.Id == id).ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _db.Users.Include(u => u.Team).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _db.Users.Include(u => u.Team).FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _db.Users.Include(u => u.Team).FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
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
            var stats = new Stats
            {
                DaysPaid = 0,
                DaysWorked = 0,
                Salary = 0,
                User = user
            };
            await _db.Stats.AddAsync(stats);
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

        public async Task AssignUserToUserRoleAsync(User user)
        {
            await _db.UserRoles.AddAsync(new UserRole
            {
                UserId = user.Id,
                RoleId = 3
            });
            await _db.SaveChangesAsync();
        }
        public async Task AssignUserToLeaderRoleAsync(User user)
        {
            await _db.UserRoles.AddAsync(new UserRole
            {
                UserId = user.Id,
                RoleId = 2
            });
            await _db.SaveChangesAsync();
        }
        public async Task RemoveUserFromLeaderRoleAsync(User user)
        {
            var leaderRole = user.UserRoles.FirstOrDefault(ur => ur.RoleId == 2);
            if (leaderRole is not null)
            {
                _db.UserRoles.Remove(leaderRole);
            }
            await _db.SaveChangesAsync();
        }
        public async Task UpdateUserCredentials(User user, UserCredentialsDto creds)
        {
            user.FirstName = creds.FirstName; 
            user.LastName = creds.LastName;

            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }
    }
}
