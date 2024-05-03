using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services.StatsService
{
    public class StatsService : IStatsService
    {
        private readonly AppDbContext _db;
        public StatsService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<Stats> GetStatsByUserIdAsync(int userId)
        {
            return await _db.Stats.FirstOrDefaultAsync(s => s.UserId == userId);
        }
        public async Task<List<Stats>> GetStatsByTeamIdAsync(int teamId)
        {
            return await _db.Stats
                .Include(s => s.User)
                .ThenInclude(u => u.Team)
                .Where(s => s.User.Team.Id == teamId)
                .ToListAsync();
        }
        public async Task UpdateUserSalaryAsync(Stats stats, int newSalary)
        {
            stats.Salary = newSalary;
            _db.Stats.Update(stats);
            await _db.SaveChangesAsync();
        }
        public async Task AddDayWorkedAsync(Stats stats)
        {
            stats.DaysWorked++;
            _db.Update(stats);
            await _db.SaveChangesAsync();
        }
        public async Task AddDayPaidAsync(Stats stats)
        {
            stats.DaysPaid++;
            _db.Update(stats);
            await _db.SaveChangesAsync();
        }
        public async Task ResetStatsOnDisbandAsync(Team team)
        {
            foreach (var user in team.Users)
            {
                user.Stats.Salary = 0;
                user.Stats.DaysWorked = 0;
                user.Stats.DaysPaid = 0;
            }
            _db.Teams.Update(team);
            await _db.SaveChangesAsync();
        }

        public async Task ResetStatsOnLeaveAsync(Stats stats)
        {
            stats.Salary = 0;
            stats.DaysWorked = 0;
            stats.DaysPaid = 0;

            _db.Stats.Update(stats);
            await _db.SaveChangesAsync();
        }
    }
}
