using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services.TeamService
{
    public class TeamService : ITeamService
    {
        private readonly AppDbContext _db;

        public TeamService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Team>> GetTeamsAsync()
        {
            return await _db.Teams.Include(t => t.Users).ThenInclude(u => u.UserRoles).ThenInclude(ur => ur.Role).ToListAsync();
        }

        public async Task<Team> GetTeamByNameAsync(string name) 
        {
            return await _db.Teams.FirstOrDefaultAsync(t => t.Name == name);
        }

        public async Task CreateTeamAsync(Team team, User user)
        {
            team.Users.Add(user);
            await _db.Teams.AddAsync(team);
            await _db.SaveChangesAsync();
        }
    }
}
