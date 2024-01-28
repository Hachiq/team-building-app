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
            return await _db.Teams.ToListAsync();
        }
    }
}
