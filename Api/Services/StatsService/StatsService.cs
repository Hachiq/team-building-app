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
    }
}
